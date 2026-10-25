using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Profile;
using UnityEditor.Build.Reporting;
using UnityEditor.XR.Management;
using UnityEditor.XR.Management.Metadata;
using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.XR.OpenXR;

namespace BirdLife.Foundation.Editor
{
    // Android profiles share XR settings: apply only the selected foundation policy.
    [InitializeOnLoad]
    public sealed class FoundationBuildGuard : IPreprocessBuildWithReport
    {
        const string Folder = "Assets/Settings/Build Profiles/";
        const string Loader = "UnityEngine.XR.OpenXR.OpenXRLoader";
        const string TransitionKey = "BirdLife.Foundation.PlayTransition";
        static bool stableEditMode;
        public int callbackOrder => -1000;
        static FoundationBuildGuard()
        {
            // SessionState survives the domain reload on entering/exiting Play.
            stableEditMode = !SessionState.GetBool(TransitionKey, false)
                && !EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode;
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
            UnityEditor.Compilation.CompilationPipeline.compilationFinished += _ => QueueSynchronization();
            QueueSynchronization();
        }

        static bool IsQuest(BuildProfile p) => AssetDatabase.GetAssetPath(p) == Folder + "QuestStandalone.asset";
        static bool IsKnown(BuildProfile p) => p && (IsQuest(p) || AssetDatabase.GetAssetPath(p) == Folder + "AndroidTouch.asset");
        public static string Output(bool quest) => Path.GetFullPath("Builds/" + (quest ? "QuestStandalone/QuestStandalone.apk" : "AndroidTouch/AndroidTouch.apk"));

        static bool CanSynchronize => stableEditMode && !SessionState.GetBool(TransitionKey, false)
            && !EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode
            && !EditorApplication.isCompiling && !EditorApplication.isUpdating
            && !BuildPipeline.isBuildingPlayer;

        static void OnPlayModeChanged(PlayModeStateChange state)
        {
            stableEditMode = state == PlayModeStateChange.EnteredEditMode;
            SessionState.SetBool(TransitionKey, !stableEditMode);
            EditorApplication.delayCall -= SynchronizeOnce;
            if (stableEditMode) QueueSynchronization();
        }

        static void QueueSynchronization()
        {
            EditorApplication.delayCall -= SynchronizeOnce;
            EditorApplication.delayCall += SynchronizeOnce;
        }

        static void SynchronizeOnce()
        {
            // One attempt per lifecycle/selection event; never poll or retry in update.
            if (!CanSynchronize || !IsKnown(BuildProfile.GetActiveBuildProfile())) return;
            try { ApplyActive(); }
            catch (Exception e) { Debug.LogError("Foundation profile configuration failed: " + e.Message); }
        }

        [MenuItem("BirdLife/Select AndroidTouch")]
        public static void SelectTouch() => Select(false);
        [MenuItem("BirdLife/Select QuestStandalone")]
        public static void SelectQuest() => Select(true);
        static void Select(bool quest)
        {
            RequireStableEditMode();
            var profile = AssetDatabase.LoadAssetAtPath<BuildProfile>(Folder + (quest ? "QuestStandalone.asset" : "AndroidTouch.asset"));
            if (!profile) throw new InvalidOperationException("Foundation profile asset missing.");
            BuildProfile.SetActiveBuildProfile(profile);
            QueueSynchronization(); // Compilation/reload also queues one attempt if needed.
        }

        static void RequireStableEditMode()
        {
            if (!CanSynchronize)
                throw new InvalidOperationException("Foundation XR synchronization requires stable Edit Mode; stop Play and wait for compilation/import to finish.");
        }

        public static void ApplyActive()
        {
            RequireStableEditMode(); // Must precede every settings/loader mutation, including direct calls.
            var profile = BuildProfile.GetActiveBuildProfile();
            if (!IsKnown(profile)) throw new InvalidOperationException("Select a foundation Build Profile first.");
            bool quest = IsQuest(profile);
            var storePath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t:XRGeneralSettingsPerBuildTarget").First());
            var store = AssetDatabase.LoadAssetAtPath<XRGeneralSettingsPerBuildTarget>(storePath);
            if (!store.HasSettingsForBuildTarget(BuildTargetGroup.Android)) store.CreateDefaultSettingsForBuildTarget(BuildTargetGroup.Android);
            if (!store.HasManagerSettingsForBuildTarget(BuildTargetGroup.Android)) store.CreateDefaultManagerSettingsForBuildTarget(BuildTargetGroup.Android);
            var general = store.SettingsForBuildTarget(BuildTargetGroup.Android);
            var manager = general.Manager;
            if (quest)
            {
                if (!manager.activeLoaders.Any(l => l && l.GetType().FullName == Loader)
                    && !XRPackageMetadataStore.AssignLoader(manager, Loader, BuildTargetGroup.Android))
                    throw new InvalidOperationException("Cannot assign OpenXR loader.");
            }
            else
            {
                foreach (var loader in manager.activeLoaders.ToArray())
                    if (!manager.TryRemoveLoader(loader)) throw new InvalidOperationException("Cannot remove touch XR loader.");
            }
            var serialized = new SerializedObject(general);
            serialized.FindProperty("m_InitManagerOnStart").boolValue = quest;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            manager.automaticLoading = quest;
            manager.automaticRunning = quest;
            var xr = OpenXRSettings.GetSettingsForBuildTargetGroup(BuildTargetGroup.Android);
            foreach (var feature in xr.GetFeatures<UnityEngine.XR.OpenXR.Features.OpenXRFeature>())
            {
                bool required = feature.GetType().Name == "MetaQuestFeature" || feature.GetType().Name == "OculusTouchControllerProfile";
                if (feature.enabled != (quest && required)) { feature.enabled = quest && required; EditorUtility.SetDirty(feature); }
            }
            EditorUtility.SetDirty(general);
            EditorUtility.SetDirty(manager);
            EditorUtility.SetDirty(store);
            EditorUtility.SetDirty(xr);
            EditorUserBuildSettings.SetBuildLocation(BuildTarget.Android, Output(quest));
            AssetDatabase.SaveAssets(); // Never saves scenes.
        }

        public static string ValidateActive()
        {
            var profile = BuildProfile.GetActiveBuildProfile();
            if (!IsKnown(profile)) return "Unknown profile: select BirdLife/Select AndroidTouch or QuestStandalone.";
            bool quest = IsQuest(profile);
            string define = quest ? "BIRDLIFE_QUEST" : "BIRDLIFE_TOUCH";
            if (!profile.scriptingDefines.Contains(define) || profile.scriptingDefines.Contains(quest ? "BIRDLIFE_TOUCH" : "BIRDLIFE_QUEST")) return "Profile mode defines mismatch.";
            if (profile.GetScenesForBuild().Count(s => s.enabled) != 1 || !profile.GetScenesForBuild().Any(s => s.enabled && s.path == "Assets/Scenes/FoundationSmoke.unity")) return "Smoke scene list mismatch.";
            var general = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(BuildTargetGroup.Android);
            if (!general || !general.Manager) return "Android XR settings missing.";
            var property = new SerializedObject(general).FindProperty("m_InitManagerOnStart");
            if (property.boolValue != quest || general.Manager.automaticLoading != quest || general.Manager.automaticRunning != quest) return "XR startup policy mismatch.";
            var loaders = general.Manager.activeLoaders;
            if (quest ? loaders.Count != 1 || loaders[0].GetType().FullName != Loader : loaders.Count != 0) return "XR loader policy mismatch.";
            var xr = OpenXRSettings.GetSettingsForBuildTargetGroup(BuildTargetGroup.Android);
            foreach (var f in xr.GetFeatures<UnityEngine.XR.OpenXR.Features.OpenXRFeature>())
            {
                bool required = f.GetType().Name == "MetaQuestFeature" || f.GetType().Name == "OculusTouchControllerProfile";
                if (f.enabled != (quest && required)) return "OpenXR feature policy mismatch: " + f.name;
            }
            return null;
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.Android) return; // Future Windows builds remain possible.
            var error = ValidateActive();
            if (error != null) throw new BuildFailedException(error);
            var expected = Output(IsQuest(BuildProfile.GetActiveBuildProfile()));
            if (!string.Equals(Path.GetFullPath(report.summary.outputPath), expected, StringComparison.OrdinalIgnoreCase))
                throw new BuildFailedException("Use the selected profile output: " + expected);
        }
    }
}


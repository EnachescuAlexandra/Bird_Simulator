using UnityEngine;
using UnityEngine.SceneManagement;

namespace BirdLife.Foundation
{
    [DefaultExecutionOrder(-1000)]
    public sealed class FoundationMode : MonoBehaviour
    {
        public enum ModeId { AndroidTouch, QuestStandalone }
        [SerializeField] private ModeId selectedMode =
#if BIRDLIFE_QUEST && !UNITY_EDITOR
            ModeId.QuestStandalone;
#else
            ModeId.AndroidTouch;
#endif
        private GameObject nonVRCamera, nonVRCanvas, nonVREvents, vrRig;
        private ModeId? appliedMode;
        public ModeId SelectedMode => selectedMode;

        // Bind only this smoke scene; no Android platform inference or build configuration.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Bootstrap()
        {
            var scene = SceneManager.GetActiveScene();
            if (scene.name != "FoundationSmoke") return;
            foreach (var root in scene.GetRootGameObjects())
                if (root.GetComponent<FoundationMode>()) return;
            var host = new GameObject("FoundationMode");
            SceneManager.MoveGameObjectToScene(host, scene);
            host.AddComponent<FoundationMode>();
        }

        private void Awake()
        {
            foreach (var root in gameObject.scene.GetRootGameObjects())
            {
                if (root.name == "Main Camera") nonVRCamera = root;
                else if (root.name == "FoundationCanvas") nonVRCanvas = root;
                else if (root.name == "FoundationEventSystem") nonVREvents = root;
                else if (root.name == "VRRig") vrRig = root;
            }
            if (!nonVRCamera || !nonVRCanvas || !vrRig)
            {
                Debug.LogError("FoundationMode requires Main Camera, FoundationCanvas and VRRig.", this);
                enabled = false;
                return;
            }
            SelectMode(selectedMode);
        }

        private void Update()
        {
            if (appliedMode != selectedMode) SelectMode(selectedMode);
        }

        public void SelectMode(ModeId mode)
        {
            if (!nonVRCamera || !nonVRCanvas || !vrRig) return;
            if (appliedMode == mode) return;
            // Disable the old action sources and camera before enabling the new set.
            nonVRCanvas.SetActive(false);
            if (nonVREvents) nonVREvents.SetActive(false);
            nonVRCamera.SetActive(false);
            vrRig.SetActive(false);
            selectedMode = mode;
            if (mode == ModeId.QuestStandalone) vrRig.SetActive(true);
            else
            {
                nonVRCamera.SetActive(true);
                nonVRCanvas.SetActive(true);
                if (nonVREvents) nonVREvents.SetActive(true);
            }
            appliedMode = mode;
        }
    }
}



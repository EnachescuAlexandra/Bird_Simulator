using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.XR.CoreUtils;

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
        [SerializeField] private GameObject nonVRCamera;
        [SerializeField] private GameObject nonVRCanvas;
        [SerializeField] private GameObject nonVREvents;
        [SerializeField] private GameObject vrRig;
        // Optional static-scene setup when the smoke input adapter is not used.
        [SerializeField] private GameObject simulator;
        [SerializeField] private Transform observationPoint;
        private ModeId? appliedMode;
        private Coroutine observationPlacement;
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
                if (!nonVRCamera && root.name == "Main Camera") nonVRCamera = root;
                else if (!nonVRCanvas && root.name == "FoundationCanvas") nonVRCanvas = root;
                else if (!nonVREvents && root.name == "FoundationEventSystem") nonVREvents = root;
                else if (!vrRig && root.name == "VRRig") vrRig = root;
            }
            if (!nonVRCamera || !vrRig || (gameObject.scene.name == "FoundationSmoke" && !nonVRCanvas)
                || (observationPoint && !vrRig.GetComponent<XROrigin>()))
            {
                Debug.LogError("FoundationMode requires a non-VR camera and VRRig; FoundationSmoke also requires FoundationCanvas. Observation placement requires an XR Origin.", this);
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
            if (!nonVRCamera || !vrRig) return;
            if (appliedMode == mode) return;
            if (observationPlacement != null)
            {
                StopCoroutine(observationPlacement);
                observationPlacement = null;
            }
            // Disable the old action sources and camera before enabling the new set.
            if (nonVRCanvas) nonVRCanvas.SetActive(false);
            if (nonVREvents) nonVREvents.SetActive(false);
            nonVRCamera.SetActive(false);
            vrRig.SetActive(false);
            if (simulator) simulator.SetActive(Application.isEditor && mode == ModeId.QuestStandalone);
            selectedMode = mode;
            if (mode == ModeId.QuestStandalone)
            {
                vrRig.SetActive(true);
                if (observationPoint) observationPlacement = StartCoroutine(PlaceObserver());
            }
            else
            {
                nonVRCamera.SetActive(true);
                if (nonVRCanvas) nonVRCanvas.SetActive(true);
                if (nonVREvents) nonVREvents.SetActive(true);
            }
            appliedMode = mode;
        }

        private IEnumerator PlaceObserver()
        {
            // Wait for the simulator's initial pose, then translate only the untracked origin.
            yield return null;
            vrRig.GetComponent<XROrigin>().MoveCameraToWorldLocation(observationPoint.position);
            observationPlacement = null;
        }
    }
}



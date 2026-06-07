using UnityEngine;
using UnityEngine.Events;

namespace PrototypeSubMod.MiscMonobehaviors;

[RequireComponent(typeof(CyclopsExternalCams))]
internal class OnExternalCamsChanged : MonoBehaviour
{
    [SerializeField] private UnityEvent onCamsEnabled;
    [SerializeField] private UnityEvent onCamsDisabled;

    private CyclopsExternalCams externalCams;
    private bool enabledLastFrame;

    private void Start()
    {
        externalCams = GetComponent<CyclopsExternalCams>();
    }

    private void LateUpdate()
    {
        if (externalCams.GetUsingCameras() != enabledLastFrame)
        {
            if (externalCams.GetUsingCameras()) onCamsEnabled?.Invoke();
            if (!externalCams.GetUsingCameras()) onCamsDisabled?.Invoke();
        }

        enabledLastFrame = externalCams.GetUsingCameras();
    }
}

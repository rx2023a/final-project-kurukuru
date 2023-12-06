using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRRayInteractorActivator : MonoBehaviour
{
    [SerializeField] InputActionProperty leftHandActivateInput;
    [SerializeField] XRRayInteractor leftHandRayInteractor;

    [SerializeField] InputActionProperty rightHandActivateInput;
    [SerializeField] XRRayInteractor rightHandRayInteractor;

    void Update()
    {
        if (leftHandActivateInput.action.WasPressedThisFrame()) {
            leftHandRayInteractor.enabled = !leftHandRayInteractor.enabled;
        }
        if (rightHandActivateInput.action.WasPressedThisFrame()) {
            rightHandRayInteractor.enabled = !rightHandRayInteractor.enabled;
        }
    }
}

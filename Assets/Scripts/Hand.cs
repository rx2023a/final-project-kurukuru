using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    [SerializeField] InputActionProperty pinchInputAction;
    [SerializeField] InputActionProperty gripInputAction;
    
    Animator handAnimator;

    void Awake() {
        handAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (handAnimator == null) return;

        float pinchValue = pinchInputAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", pinchValue);

        float gripValue = gripInputAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}

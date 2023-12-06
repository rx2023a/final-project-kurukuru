using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLookDebug : MonoBehaviour
{
    [SerializeField] InputActionProperty mouseInput;
    [SerializeField] InputActionProperty keyboardInput;

    [SerializeField] float keyboardSpeed = 1f;
    [SerializeField] float mouseSpeed = 1f;

    void Update()
    {
        Vector2 mouseInputValue = mouseInput.action.ReadValue<Vector2>();
        Vector2 keyboardInputValue = keyboardInput.action.ReadValue<Vector2>();
        var newRot = transform.rotation.eulerAngles + new Vector3(
            -mouseInputValue.y * mouseSpeed, 
            mouseInputValue.x * mouseSpeed, 
            0);
        transform.rotation = Quaternion.Euler(newRot);

        Vector3 translation = Vector3.zero;
        translation += keyboardInputValue.x * keyboardSpeed * Time.deltaTime * transform.InverseTransformDirection(transform.right);
        translation += keyboardInputValue.y * keyboardSpeed * Time.deltaTime * transform.InverseTransformDirection(transform.forward);
        transform.Translate(translation);
    }
}

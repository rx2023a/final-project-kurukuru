using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float keyboardSpeed = 1f;
    [SerializeField] float mouseSpeed = 1f;

    void Update()
    {
        var newRot = transform.rotation.eulerAngles + new Vector3(
            -Input.GetAxis("Mouse Y") * mouseSpeed, 
            Input.GetAxis("Mouse X") * mouseSpeed, 
            0);
        transform.rotation = Quaternion.Euler(newRot);

        Vector3 translation = Vector3.zero;
        translation += Input.GetAxis("Horizontal") * keyboardSpeed * Time.deltaTime * transform.InverseTransformDirection(transform.right);
        translation += Input.GetAxis("Vertical") * keyboardSpeed * Time.deltaTime * transform.InverseTransformDirection(transform.forward);
        transform.Translate(translation);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyTrigger : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] List<Transform> targets = new();
    [SerializeField] float speed = 1f;
    [SerializeField] FlyTriggerMode mode;

    void OnDrawGizmos() {
        if (mode == null) return;

        mode.Draw(head, targets);
    }

    void Update() {
        if (mode == null) return;

        if (DetectTargets()) {
            Vector3 sumDir = Vector3.zero;
            foreach(Transform target in targets) {
                sumDir += target.position - head.position;
            }
            transform.position += speed * Time.deltaTime * sumDir;
        }
    }

    public bool DetectTargets() {
        return mode.DetectTargets(head, targets);
    }
}

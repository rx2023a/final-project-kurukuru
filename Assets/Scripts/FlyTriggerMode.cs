using System.Collections.Generic;
using UnityEngine;

public abstract class FlyTriggerMode : ScriptableObject {
    public abstract void Draw(Transform transform, Transform target);
    public abstract void Draw(Transform transform, IEnumerable<Transform> targets);
    public abstract bool DetectTarget(Transform transform, Transform target);

    public bool DetectTargets(Transform transform, IEnumerable<Transform> targets) {
        foreach(Transform target in targets) {
            if(!DetectTarget(transform, target)) return false;
        }
        return true;
    }
}
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SphericalMode", menuName = "Digest/Movement/SphericalMode", order = 0)]
public class SphericalMode : FlyTriggerMode {
    [SerializeField] [Min(0)] float minRadius = 1f;
    [SerializeField] [Min(0)] float maxRadius = 4f;

    public override void Draw(Transform transform, Transform target)
    {
        Gizmos.color = DetectTarget(transform, target) ? Color.green : Color.red;
        DrawGizmos(transform);
    }

    public override void Draw(Transform transform, IEnumerable<Transform> targets)
    {
        Gizmos.color = DetectTargets(transform, targets) ? Color.green : Color.red;
        DrawGizmos(transform);
    }

    public override bool DetectTarget(Transform transform, Transform target) {
        if (target == null) return false;

        Vector3 targetLocalPosition = transform.InverseTransformPoint(target.position);

        if (IsInRadius(targetLocalPosition, minRadius)) return false;

        if (!IsInRadius(targetLocalPosition, maxRadius)) return false;

        return true;
    }

    void DrawGizmos(Transform transform)
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawWireSphere(Vector3.zero, minRadius);
        Gizmos.DrawWireSphere(Vector3.zero, maxRadius);
    }

    bool IsInRadius(Vector3 localPosition, float radius) {
        return localPosition.magnitude <= radius;
    }
}
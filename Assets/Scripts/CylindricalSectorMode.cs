using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CylindricalSectorMode", menuName = "Digest/Movement/CylindricalSectorMode", order = 0)]
public class CylindricalSectorMode : FlyTriggerMode {
    [SerializeField] [Min(0)] float minRadius = 1f;
    [SerializeField] [Min(0)] float maxRadius = 4f;
    [SerializeField] float height = 2f;
    [SerializeField] [Range(0, 180f)] float angDeg = 90f;

    float Width => Mathf.Sin(angDeg / 2 * Mathf.Deg2Rad);

    public override void Draw(Transform transform, Transform target)
    {
        Gizmos.color = Handles.color = DetectTarget(transform, target) ? Color.green : Color.red;
        DrawGizmos(transform);
    }

    public override void Draw(Transform transform, IEnumerable<Transform> targets)
    {
        Gizmos.color = Handles.color = DetectTargets(transform, targets) ? Color.green : Color.red;
        DrawGizmos(transform);
    }

    public override bool DetectTarget(Transform transform, Transform target) {
        if (target == null) return false;

        Vector3 targetLocalPosition = transform.InverseTransformPoint(target.position);

        if (IsInRadius(targetLocalPosition, minRadius)) return false;

        if (!IsInRadius(targetLocalPosition, maxRadius)) return false;
        if (!IsInHeight(targetLocalPosition)) return false;
        if (!IsInFront(targetLocalPosition)) return false;

        return true;
    }

    void DrawGizmos(Transform transform) {
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        float x = Width;
        float y = Mathf.Sqrt(1 - x * x);

        Vector3 bottom = new(0f, -height / 2, 0f);
        Vector3 up = new(0f, height / 2, 0f);
        Vector3 rightMin = new Vector3(x, 0, y) * minRadius;
        Vector3 leftMin = new Vector3(-x, 0, y) * minRadius;
        Vector3 rightMax = new Vector3(x, 0, y) * maxRadius;
        Vector3 leftMax = new Vector3(-x, 0, y) * maxRadius;

        Gizmos.DrawLine(bottom + rightMin, bottom + rightMax);
        Gizmos.DrawLine(bottom + leftMin, bottom + leftMax);

        Gizmos.DrawLine(up + rightMin, up + rightMax);
        Gizmos.DrawLine(up + leftMin, up + leftMax);

        Gizmos.DrawLine(bottom + leftMin, up + leftMin);
        Gizmos.DrawLine(bottom + rightMin, up + rightMin);
        Gizmos.DrawLine(bottom + leftMax, up + leftMax);
        Gizmos.DrawLine(bottom + rightMax, up + rightMax);

        Handles.DrawWireArc(bottom, Vector3.up, leftMin, angDeg, minRadius);
        Handles.DrawWireArc(up, Vector3.up, leftMin, angDeg, minRadius);
        Handles.DrawWireArc(bottom, Vector3.up, leftMax, angDeg, maxRadius);
        Handles.DrawWireArc(up, Vector3.up, leftMax, angDeg, maxRadius);
    }

    bool IsInRadius(Vector3 localPosition, float radius) {
        return localPosition.magnitude <= radius;
    }

    bool IsInHeight(Vector3 localPosition) {
        return Mathf.Abs(localPosition.y) <= height/2;
    }

    bool IsInFront(Vector3 localPosition) {
        Vector3 toTarget = new Vector3(localPosition.x, 0, localPosition.z).normalized;
        if (Vector3.Dot(Vector3.forward, toTarget) < 0) return false;
        return Vector3.Cross(Vector3.forward, toTarget).magnitude <= Width;
    }
}
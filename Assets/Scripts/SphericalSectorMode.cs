using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SphericalSectorMode", menuName = "Digest/Movement/SphericalSectorMode", order = 0)]
public class SphericalSectorMode : FlyTriggerMode {
    [SerializeField] [Min(0)] float minRadius = 1f;
    [SerializeField] [Min(0)] float maxRadius = 4f;
    [SerializeField] [Range(0f, 180f)] float angDeg = 90;

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
        if (!IsInFront(targetLocalPosition, Mathf.Sin(angDeg/2 * Mathf.Deg2Rad))) return false;

        return true;
    }

    void DrawGizmos(Transform transform)
    {
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        float x = Mathf.Sin(angDeg / 2 * Mathf.Deg2Rad);
        float y = x;
        float z = Mathf.Cos(angDeg / 2 * Mathf.Deg2Rad);

        Vector3 rightMin = new Vector3(x, 0, z) * minRadius;
        Vector3 leftMin = new Vector3(-x, 0, z) * minRadius;
        Vector3 upMin = new Vector3(0, y, z) * minRadius;
        Vector3 downMin = new Vector3(0, -y, z) * minRadius;

        Vector3 rightMax = new Vector3(x, 0, z) * maxRadius;
        Vector3 leftMax = new Vector3(-x, 0, z) * maxRadius;
        Vector3 upMax = new Vector3(0, y, z) * maxRadius;
        Vector3 downMax = new Vector3(0, -y, z) * maxRadius;

        Gizmos.DrawLine(rightMin, rightMax);
        Gizmos.DrawLine(leftMin, leftMax);
        Gizmos.DrawLine(upMin, upMax);
        Gizmos.DrawLine(downMin, downMax);

        Handles.DrawWireDisc(new Vector3(0, 0, z) * maxRadius, Vector3.forward, x * maxRadius);
        Handles.DrawWireArc(Vector3.zero, Vector3.right, upMax, angDeg, maxRadius);
        Handles.DrawWireArc(Vector3.zero, Vector3.up, leftMax, angDeg, maxRadius);

        Handles.DrawWireDisc(new Vector3(0, 0, z) * minRadius, Vector3.forward, x * minRadius);
        Handles.DrawWireArc(Vector3.zero, Vector3.right, upMax, angDeg, minRadius);
        Handles.DrawWireArc(Vector3.zero, Vector3.up, leftMax, angDeg, minRadius);
    }

    bool IsInRadius(Vector3 localPosition, float radius) {
        return localPosition.magnitude <= radius;
    }

    bool IsInFront(Vector3 localPosition, float treshold) {
        Vector3 toTarget = localPosition.normalized;
        if (Vector3.Dot(Vector3.forward, toTarget) < 0) return false;
        return Vector3.Cross(Vector3.forward, toTarget).magnitude <= treshold;
    }
}
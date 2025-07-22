using UnityEngine;
using System.Collections.Generic;

public class WeaponBoxCast : MonoBehaviour
{
    public Transform[] points = new Transform[4];
    public Vector3 castDirection = Vector3.forward;
    public float castDistance = 10f;
    public LayerMask collisionMask;
    public Color defaultFillColor = new Color(1f, 1f, 0f, 0.3f);
    public Color hitFillColor = new Color(0f, 1f, 0f, 0.3f);
    public int fillSteps = 20;
    public bool hitSomething;
    public RaycastHit hitInfo;
    public Transform weapon;

    private List<Vector3> prevPoints = new List<Vector3>();
    private List<Vector3> currentPoints = new List<Vector3>();

    private void Start()
    {
        Time.timeScale = 1;
        for (int i = 0; i < points.Length; i++)
        {
            prevPoints.Add(points[i].position);
            currentPoints.Add(points[i].position);
        }
    }

    void Update()
    {
        if (!ValidatePoints())
            return;

        // **Apply weapon's position and rotation correctly**
        transform.position = weapon.position;
        transform.rotation = weapon.rotation; // ✅ Use rotation, not eulerAngles

        // Compute local bounding box in local space.
        Vector3 localMin, localMax;
        GetLocalBoundingBox(out localMin, out localMax);
        Vector3 localCenter = (localMin + localMax) / 2f;
        Vector3 localHalfExtents = (localMax - localMin) / 2f;

        // Convert center and extents to world space.
        Vector3 worldCenter = transform.TransformPoint(localCenter);
        Quaternion worldRotation = transform.rotation;
        Vector3 worldHalfExtents = RotateExtents(localHalfExtents, worldRotation);

        // Use transformed weapon's forward as the casting direction
        Vector3 worldDir = transform.forward; // ✅ Correct direction from weapon rotation

        // Perform BoxCast
        hitSomething = Physics.BoxCast(worldCenter, worldHalfExtents, worldDir, out hitInfo, worldRotation, castDistance, collisionMask);

        if (hitSomething && weapon.GetComponent<WeaponCollider>().canCollide)
        {
            Debug.Log("Collided with: " + hitInfo.collider.name);
            weapon.GetComponent<WeaponCollider>().canCollide = false;
        }

        // Update previous positions
        prevPoints = new List<Vector3>(currentPoints);
        for (int i = 0; i < points.Length; i++)
        {
            currentPoints[i] = points[i].position;
        }
    }

    private bool ValidatePoints()
    {
        if (points.Length != 4 || points[0] == null || points[1] == null || points[2] == null || points[3] == null)
        {
            Debug.LogError("Please assign exactly four Transform components to the 'points' array.");
            return false;
        }
        return true;
    }

    private void GetLocalBoundingBox(out Vector3 localMin, out Vector3 localMax)
    {
        localMin = transform.InverseTransformPoint(points[0].position);
        localMax = localMin;
        for (int i = 1; i < 4; i++)
        {
            Vector3 localPos = transform.InverseTransformPoint(points[i].position);
            localMin = Vector3.Min(localMin, localPos);
            localMax = Vector3.Max(localMax, localPos);
        }
    }

    private Vector3 RotateExtents(Vector3 halfExtents, Quaternion rotation)
    {
        Matrix4x4 rotMat = Matrix4x4.Rotate(rotation);
        return rotMat.MultiplyVector(halfExtents);
    }

    private void OnDrawGizmos()
    {
        if (!ValidatePoints())
            return;

        Vector3 localMin, localMax;
        GetLocalBoundingBox(out localMin, out localMax);
        Vector3 localCenter = (localMin + localMax) / 2f;
        Vector3 localHalfExtents = (localMax - localMin) / 2f;

        Vector3 worldCenter = transform.TransformPoint(localCenter);
        Quaternion worldRotation = transform.rotation;
        Vector3 worldHalfExtents = RotateExtents(localHalfExtents, worldRotation);
        Vector3 worldDir = transform.forward;
        Vector3 endCenter = worldCenter + worldDir * castDistance;

        Color fillColor = hitSomething ? hitFillColor : defaultFillColor;

        // **Draw start and end boxes**
        Gizmos.matrix = Matrix4x4.TRS(worldCenter, worldRotation, Vector3.one);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, worldHalfExtents * 2f);
        Gizmos.matrix = Matrix4x4.identity;

        Gizmos.matrix = Matrix4x4.TRS(endCenter, worldRotation, Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, worldHalfExtents * 2f);
        Gizmos.matrix = Matrix4x4.identity;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(worldCenter, endCenter);

        // **Fill cast volume**
        Gizmos.color = fillColor;
        for (int i = 0; i <= fillSteps; i++)
        {
            float t = (float)i / fillSteps;
            Vector3 interpCenter = Vector3.Lerp(worldCenter, endCenter, t);
            Gizmos.matrix = Matrix4x4.TRS(interpCenter, worldRotation, Vector3.one);
            Gizmos.DrawCube(Vector3.zero, worldHalfExtents * 2f);
        }
        Gizmos.matrix = Matrix4x4.identity;

        if (hitSomething)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(hitInfo.point, 0.2f);
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(hitInfo.point, hitInfo.normal);
        }
    }
}

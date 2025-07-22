using UnityEngine;

public class BoxCastFromPoints : MonoBehaviour
{
    public Transform[] points = new Transform[4];
    public Vector3 castDirection = Vector3.forward;
    public float castDistance = 10f;
    public LayerMask collisionMask;
    public Color defaultFillColor = new Color(1f, 1f, 0f, 0.3f); // Semi-transparent yellow
    public Color hitFillColor = new Color(0f, 1f, 0f, 0.3f);     // Semi-transparent green on hit
    public int fillSteps = 20;  // Number of cubes along the cast path
    public bool hitSomething;
    public RaycastHit hitInfo;
   

    void Update()
    {
        if (!ValidatePoints())
            return;


        
        // Compute local bounding box in our own local space.
        Vector3 localMin, localMax;
        GetLocalBoundingBox(out localMin, out localMax);
        Vector3 localCenter = (localMin + localMax) / 2f;
        Vector3 localHalfExtents = (localMax - localMin) / 2f;

        // Convert center and extents to world space.
        Vector3 worldCenter = transform.TransformPoint(localCenter);
        Quaternion worldRotation = transform.rotation;
        Vector3 worldHalfExtents = RotateExtents(localHalfExtents, worldRotation);

        // First check: if our starting volume overlaps any colliders that are not part of our own hierarchy.
        Collider[] overlaps = Physics.OverlapBox(worldCenter, worldHalfExtents, worldRotation, collisionMask);
        bool initialOverlap = false;
        foreach (Collider col in overlaps)
        {
            // Ignore colliders that are part of this object (or its children).
            if (!col.transform.IsChildOf(transform))
            {
                initialOverlap = true;
                // Optionally set hitInfo.point to the closest point.
                hitInfo.point = col.ClosestPoint(worldCenter);
                break;
            }
        }

        if (initialOverlap)
        {
            hitSomething = true;
        }
        else
        {
            // Only perform BoxCast if no initial overlap from external colliders.
            Vector3 worldDir = transform.TransformDirection(castDirection.normalized);
            hitSomething = Physics.BoxCast(worldCenter, worldHalfExtents, worldDir, out hitInfo, worldRotation, castDistance, collisionMask);
        }
    }

    private bool ValidatePoints()
    {
        if (points.Length != 4 ||
            points[0] == null || points[1] == null || points[2] == null || points[3] == null)
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

    // Rotate half extents using the given rotation.
    private Vector3 RotateExtents(Vector3 halfExtents, Quaternion rotation)
    {
        Matrix4x4 rotMat = Matrix4x4.Rotate(rotation);
        return rotMat.MultiplyVector(halfExtents);
    }

    private void OnDrawGizmos()
    {
        if (!ValidatePoints())
            return;

        // Compute local bounding box.
        Vector3 localMin, localMax;
        GetLocalBoundingBox(out localMin, out localMax);
        Vector3 localCenter = (localMin + localMax) / 2f;
        Vector3 localHalfExtents = (localMax - localMin) / 2f;

        // Convert center and extents to world space.
        Vector3 worldCenter = transform.TransformPoint(localCenter);
        Quaternion worldRotation = transform.rotation;
        Vector3 worldHalfExtents = RotateExtents(localHalfExtents, worldRotation);
        Vector3 worldDir = transform.TransformDirection(castDirection.normalized);
        Vector3 endCenter = worldCenter + worldDir * castDistance;

        // Choose fill color based on hit.
        Color fillColor = hitSomething ? hitFillColor : defaultFillColor;

        // Draw the starting box (wireframe)
        Gizmos.matrix = Matrix4x4.TRS(worldCenter, worldRotation, Vector3.one);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, worldHalfExtents * 2f);
        Gizmos.matrix = Matrix4x4.identity;

        // Draw the ending box (wireframe)
        Gizmos.matrix = Matrix4x4.TRS(endCenter, worldRotation, Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, worldHalfExtents * 2f);
        Gizmos.matrix = Matrix4x4.identity;

        // Draw a line indicating the cast direction.
        Gizmos.color = Color.red;
        Gizmos.DrawLine(worldCenter, endCenter);

        // Fill the cast volume with overlapping cubes along the path.
        Gizmos.color = fillColor;
        for (int i = 0; i <= fillSteps; i++)
        {
            float t = (float)i / fillSteps;
            Vector3 interpCenter = Vector3.Lerp(worldCenter, endCenter, t);
            Gizmos.matrix = Matrix4x4.TRS(interpCenter, worldRotation, Vector3.one);
            Gizmos.DrawCube(Vector3.zero, worldHalfExtents * 2f);
        }
        Gizmos.matrix = Matrix4x4.identity;

        // Visualize hit information if detected.
        if (hitSomething)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(hitInfo.point, 0.2f);
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(hitInfo.point, hitInfo.normal);
        }
    }
}

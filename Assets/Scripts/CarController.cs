using UnityEngine;

public class CarController : MonoBehaviour
{
    public Roads roads;
    public Vertex position;

    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    private WheelCollider[] colliders;
    private Path path;

    private const float MAX_ANGLE = 45f;
    private const float MAX_TORQUE = 200f;
    private const float DISTANCE_MARGIN = 10f;

    private Color colorIntersections = Color.red;
    private float radius = 3f;

    public void Start()
    {
        Initialize();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = colorIntersections;

        if (path != null)
        {
            foreach(Vertex vertex in path.GetVertices())
            {
                Transform transform = vertex.transform;
                Vector3 position = transform.position;
                Gizmos.DrawWireSphere(position, radius);
            }
        }
    }

    void Update()
    {
        AdjustWheelPosition();
    }

    void CompleteWaypoint()
    {
        if (!path.GetNext())
        {
            SelectDestination();
        }
    }

    void AdjustWheelPosition()
    {
        Vector3 waypoint = path.GetCurrentTarget();
        waypoint.y = transform.position.y;

        Vector3 steerVector = transform.InverseTransformPoint(waypoint);
        float angle = MAX_ANGLE * (steerVector.x / steerVector.magnitude);

        frontLeft.steerAngle = angle;
        frontRight.steerAngle = angle;

        if (steerVector.magnitude < DISTANCE_MARGIN)
        {
            CompleteWaypoint();
        }
    }

    private void Initialize()
    {
        colliders = new WheelCollider[] { frontLeft, frontRight, rearLeft, rearRight };

        foreach (WheelCollider collider in colliders)
        {
            collider.ConfigureVehicleSubsteps(5, 12, 15);
        }

        rearLeft.motorTorque = MAX_TORQUE;
        rearRight.motorTorque = MAX_TORQUE;
    }

    public void SelectDestination()
    {
        path = roads.GetPathToRandomTarget(position);
    }

    private void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}

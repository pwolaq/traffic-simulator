using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private int index = 0;

    private Color colorIntersections = Color.red;
    private Color colorSegments = Color.green;
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
                Intersection i = vertex.GetIntersection();
                Transform transform = i.GetComponent<Transform>();
                Vector3 position = transform.position;
                Gizmos.DrawWireSphere(position, radius);
            }
        }
    }

    void Update()
    {
        AdjustWheelPosition();
    }

    void AdjustWheelPosition()
    {
        Transform t = path.GetCurrentTarget();
        Vector3 waypoint = new Vector3(t.position.x, transform.position.y, t.position.z);
        Vector3 steerVector = transform.InverseTransformPoint(waypoint);
        float angle = MAX_ANGLE * (steerVector.x / steerVector.magnitude);

        frontLeft.steerAngle = angle;
        frontRight.steerAngle = angle;

        if (steerVector.magnitude < DISTANCE_MARGIN)
        {
            CompleteWaypoint();
        }
    }

    private void CompleteWaypoint()
    {
        position = path.GetCurrentVertex();

        if (!path.GetNextTarget())
        {
            SelectDestination();
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

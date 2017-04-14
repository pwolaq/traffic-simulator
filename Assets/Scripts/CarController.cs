using UnityEngine;
using System.Linq;

public class CarController : MonoBehaviour
{
    public Roads roads;
    public Vertex position;
    public Transform body;
    public Rigidbody rb;

    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    private WheelCollider[] colliders;
    private Path path;
    private float torque;

    private const float MAX_ANGLE = 45f;
    private const float MAX_TORQUE = 100f;
    private const float DISTANCE_MARGIN = 2f;

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
        // Debug.Log(rb.velocity.magnitude * 3.6); // speed in km/h
    }

    void CompleteWaypoint()
    {
        if (!path.GetNext())
        {
            position = path.GetVertices().Last<Vertex>();
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

        ApplyLocalPositionToVisuals();

        if (steerVector.magnitude < DISTANCE_MARGIN)
        {
            CompleteWaypoint();
        }
    }

    private void Initialize()
    {
        torque = Random.Range(MAX_TORQUE / 2, MAX_TORQUE);
        colliders = new WheelCollider[] { frontLeft, frontRight, rearLeft, rearRight };

        foreach (WheelCollider collider in colliders)
        {
            collider.ConfigureVehicleSubsteps(5, 12, 15);
        }

        rearLeft.motorTorque = torque;
        rearRight.motorTorque = torque;

        SetRandomColor();
    }

    private void SetRandomColor()
    {
        Color color = new Color(Random.value, Random.value, Random.value, 1.0f);

        foreach (Renderer renderer in body.GetComponentsInChildren<Renderer>())
        {
            if (renderer.CompareTag("Repaintable"))
            {
                renderer.material.color = color;
                renderer.material.SetColor("_EmissionColor", color);
            }
        }
    }

    public void SelectDestination()
    {
        path = roads.GetPathToRandomTarget(position);
    }

    private void ApplyLocalPositionToVisuals()
    {
        ApplyLocalPositionToVisuals(frontLeft);
        ApplyLocalPositionToVisuals(frontRight);
        ApplyLocalPositionToVisuals(rearLeft);
        ApplyLocalPositionToVisuals(rearRight);
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

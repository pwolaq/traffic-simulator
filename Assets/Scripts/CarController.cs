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

    private const float BRAKE_TORQUE = 10f;
    private const float MAX_SPEED = 30f;
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
        AdjustSpeed();
        CheckSensors();
    }

    void CheckSensors()
    {
        Vector3 position;
        RaycastHit hit;

        position = transform.position;
        position += transform.forward * 3;
        position += transform.up;

        if (Physics.Raycast(position, transform.forward, out hit, 100))
        {
            Debug.DrawLine(position, hit.point, Color.magenta);
        }
    }
 
    void CompleteWaypoint()
    {
        if (!path.GetNext())
        {
            position = path.GetVertices().Last<Vertex>();
            SelectDestination();
        }
    }

    void AdjustSpeed()
    {
        float speed = rb.velocity.magnitude * 3.6f; // speed in km/h

        if (speed <= MAX_SPEED)
        {
            SetTorque(torque, 0);
        }
        else
        {
            SetTorque(0, 0);
        }
    }

    void SetTorque(float motor, float brake)
    {
        frontLeft.motorTorque = motor;
        frontRight.motorTorque = motor;
        frontLeft.brakeTorque = brake;
        frontRight.brakeTorque = brake;
        rearLeft.brakeTorque = brake;
        rearRight.brakeTorque = brake;
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

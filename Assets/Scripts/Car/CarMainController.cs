using UnityEngine;
using System.Linq;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Vehicles.Car;

public class CarMainController : MonoBehaviour
{
    private Roads roads;
    private Vertex position;
    public Transform body;
    private Path path;

    public void Start()
    {
        Initialize();
    }

    public void Setup(Vertex position, Roads roads)
    {
        this.position = position;
        this.roads = roads;

        var target = new GameObject(name + " Waypoint Target").transform;
        this.GetComponent<WaypointProgressTracker>().target = target;
        this.GetComponent<CarAIControl>().m_Target = target;
    }

    private void Initialize()
    {
        SetRandomColor();
        SelectDestination();
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
        position = path.finishPosition;
        WaypointProgressTracker wpt = this.GetComponent<WaypointProgressTracker>();
        wpt.Setup(new WaypointCircuit(path.GetPath()));
    }
}

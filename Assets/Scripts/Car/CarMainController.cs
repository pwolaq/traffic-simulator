using UnityEngine;
using System.Linq;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Vehicles.Car;
using System.Collections.Generic;

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
        SelectDestination(null);
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

    public Vertex GetPosition()
    {
        return position;
    }

    public void CompleteWaypoint(int n)
    {
        int index = ((n - 1) >> 2);
        position = path.GetVertex(index + 1);

        if (n == path.GetPath().Length)
        {
            SelectDestination(path.GetVertex(index));
        }
    }

    private void SelectDestination(Vertex previousVertex)
    {
        if (previousVertex == null)
        {
            path = roads.GetPathToRandomTarget(position);
        }
        else
        {
            List<Vertex> filteredVertices = new List<Vertex>();

            foreach (Edge e in position.GetEdges())
            {
                Vertex neighbor = e.GetNeighbor(position);
                if (neighbor != previousVertex)
                {
                    filteredVertices.Add(neighbor);
                }
            }

            if (filteredVertices.Count == 0)
            {
                path = roads.GetPathToRandomTarget(position);
            }
            else
            {
                Vertex randomNeighbor = filteredVertices[Random.Range(0, filteredVertices.Count)];
                path = roads.GetPathToRandomTarget(randomNeighbor);
                path.Prepend(position);
                path.Prepend(previousVertex);
            }
        }

        path.Calculate();
        WaypointProgressTracker wpt = this.GetComponent<WaypointProgressTracker>();
        wpt.Setup(new WaypointCircuit(path.GetPath()));
    }
}

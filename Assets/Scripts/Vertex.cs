using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    private List<Edge> edges = new List<Edge>();
    public TrafficLightsController lights;

    void Awake()
    {
        lights = GetComponent<TrafficLightsController>();
    }

    public bool CanGo(Vector3 position, bool veryClose)
    {
        return !this.lights || this.lights.CanGo(position, veryClose);
    }

    public void AddEdge(Edge e)
    {
        edges.Add(e);
    }

    public List<Edge> GetEdges()
    {
        return edges;
    }
}

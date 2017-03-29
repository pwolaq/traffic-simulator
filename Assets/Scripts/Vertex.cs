using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex {
    private Intersection intersection;
    private List<Edge> edges = new List<Edge>();

    public Vertex(Intersection obj)
    {
        intersection = obj;
    }

    public void AddEdge(Edge e)
    {
        edges.Add(e);
    }

    public List<Edge> GetEdges()
    {
        return edges;
    }

    public void Log()
    {
        Debug.Log(this);

        foreach(Edge e in edges)
        {
            Debug.Log(" -> " + e.GetNeighbor(this));
        }
    }

    public override string ToString()
    {
        return "Intersection #" + intersection.GetInstanceID().ToString("X");
    }
}

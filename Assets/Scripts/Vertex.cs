using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    private List<Edge> edges = new List<Edge>();

    public void AddEdge(Edge e)
    {
        edges.Add(e);
    }

    public List<Edge> GetEdges()
    {
        return edges;
    }
}

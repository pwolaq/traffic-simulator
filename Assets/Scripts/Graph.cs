using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph {
    private Dictionary<int, Vertex> g = new Dictionary<int, Vertex>();
    private List<Vertex> vertices;

    public Graph(GameObject[] v, GameObject[] e)
    {
        AddVertices(v);
        AddEdges(e);

        vertices = Enumerable.ToList(g.Values);
    }

    public Vertex GetRandomVertex()
    {
        int index = Random.Range(0, vertices.Count);
        return vertices[index];
    }

    private void AddVertices(GameObject[] vertices)
    {
        foreach (GameObject obj in vertices)
        {
            Intersection intersection = obj.GetComponent<Intersection>();
            Vertex vertex = new Vertex(intersection);
            g.Add(intersection.GetInstanceID(), vertex);
        }
    }

    private void AddEdges(GameObject[] edges)
    {
        foreach (GameObject obj in edges)
        {
            Segment segment = obj.GetComponent<Segment>();
            AddEdge(segment);
        }
    }

    private void AddEdge(Segment s)
    {
        Vertex vertexA;
        Vertex vertexB;
        
        g.TryGetValue(s.intersectionA.GetInstanceID(), out vertexA);
        g.TryGetValue(s.intersectionB.GetInstanceID(), out vertexB);

        if(vertexA != null && vertexB != null)
        {
            new Edge(vertexA, vertexB, s);
        }
    }

    public List<Vertex> ShortestPath(Vertex from, Vertex to)
    {
        return new PathFinder(g, from, to).FindDefaultPath();
    }
}

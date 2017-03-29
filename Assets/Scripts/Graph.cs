using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {
    private Dictionary<int, Vertex> g = new Dictionary<int, Vertex>();

    public Graph(GameObject[] vertices, GameObject[] edges)
    {
        foreach(GameObject obj in vertices)
        {
            Intersection intersection = obj.GetComponent<Intersection>();
            Vertex vertex = new Vertex(intersection);
            g.Add(intersection.GetInstanceID(), vertex);
        }

        foreach(GameObject obj in edges)
        {
            Segment segment = obj.GetComponent<Segment>();
            AddEdge(segment);
        }

        foreach (GameObject obj in vertices)
        {
            Intersection intersection = obj.GetComponent<Intersection>();
            Vertex vertex;
            g.TryGetValue(intersection.GetInstanceID(), out vertex);
            vertex.Log();
        }
    }

    private void AddEdge(Segment s)
    {
        Edge edge;
        Vertex vertexA;
        Vertex vertexB;
        
        g.TryGetValue(s.intersectionA.GetInstanceID(), out vertexA);
        g.TryGetValue(s.intersectionB.GetInstanceID(), out vertexB);

        if(vertexA != null && vertexB != null)
        {
            edge = new Edge(vertexA, vertexB, s);
        }
    }
}

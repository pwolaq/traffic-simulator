using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {
    private Dictionary<int, Vertex> g = new Dictionary<int, Vertex>();
    public List<Vertex> cachedPath = null;

    public Graph(GameObject[] vertices, GameObject[] edges)
    {
        AddVertices(vertices);
        AddEdges(edges);
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

    public List<Vertex> ShortestPath(Vertex from, Vertex to)
    {
        Dictionary<Vertex, int> distances = new Dictionary<Vertex, int>();
        Dictionary<Vertex, Vertex> previous = new Dictionary<Vertex, Vertex>();
        HashSet<Vertex> unvisited = new HashSet<Vertex>();

        foreach(Vertex vertex in g.Values)
        {
            distances.Add(vertex, int.MaxValue);
            previous.Add(vertex, null);
            unvisited.Add(vertex);
        }

        distances[from] = 0;

        while(unvisited.Count > 0)
        {
            Vertex vertex = null;
            int min = int.MaxValue;

            foreach(Vertex v in unvisited)
            {
                if(distances[v] < min)
                {
                    min = distances[v];
                    vertex = v;
                }
            }

            if (to.Equals(vertex))
            {
                break;
            }

            unvisited.Remove(vertex);

            VisitNeighbors(vertex, distances, previous);
        }

        return GetPath(previous, to);
    }

    private void VisitNeighbors(Vertex vertex, Dictionary<Vertex, int> distances, Dictionary<Vertex, Vertex> previous)
    {
        foreach (Edge e in vertex.GetEdges())
        {
            int alt = distances[vertex] + e.GetDistance();
            Vertex neighbor = e.GetNeighbor(vertex);

            if (alt < distances[neighbor])
            {
                distances[neighbor] = alt;
                previous[neighbor] = vertex;
            }
        }
    }

    private List<Vertex> GetPath(Dictionary<Vertex, Vertex> previous, Vertex to)
    {
        List<Vertex> path = new List<Vertex>();

        while(to != null)
        {
            path.Add(to);
            to = previous[to];
        }

        path.Reverse();
        return path;
    }
}

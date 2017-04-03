using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder {
    private Dictionary<Vertex, int> distances = new Dictionary<Vertex, int>();
    private Dictionary<Vertex, Vertex> previous = new Dictionary<Vertex, Vertex>();
    private HashSet<Vertex> unvisited = new HashSet<Vertex>();

    private Vertex from;
    private Vertex to;

    public PathFinder(Dictionary<int, Vertex> graph, Vertex vFrom, Vertex vTo)
    {
        from = vFrom;
        to = vTo;

        Initialize(graph);
    }

    public Path FindDefaultPath()
    {
        while (unvisited.Count > 0)
        {
            Vertex vertex = FindClosestUnvisitedVertex();

            if (to.Equals(vertex))
            {
                break;
            }

            unvisited.Remove(vertex);
            VisitNeighbors(vertex);
        }

        return GetPath();
    }

    private void Initialize(Dictionary<int, Vertex> graph)
    {
        foreach (Vertex vertex in graph.Values)
        {
            distances.Add(vertex, int.MaxValue);
            previous.Add(vertex, null);
            unvisited.Add(vertex);
        }

        distances[from] = 0;
    }

    private Vertex FindClosestUnvisitedVertex()
    {
        Vertex vertex = null;
        int min = int.MaxValue;

        foreach (Vertex v in unvisited)
        {
            if (distances[v] < min)
            {
                min = distances[v];
                vertex = v;
            }
        }

        return vertex;
    }

    private void VisitNeighbors(Vertex vertex)
    {
        foreach (Edge edge in vertex.GetEdges())
        {
            int alt = distances[vertex] + edge.GetDistance();
            Vertex neighbor = edge.GetNeighbor(vertex);

            if (alt < distances[neighbor])
            {
                distances[neighbor] = alt;
                previous[neighbor] = vertex;
            }
        }
    }

    private Path GetPath()
    {
        List<Vertex> path = new List<Vertex>();

        while (to != null)
        {
            path.Add(to);
            to = previous[to];
        }

        path.Reverse();

        return new Path(path);
    }
}

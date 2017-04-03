using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph {
    private Vertex[] vertices;

    public Graph(Vertex[] v)
    {
        vertices = v;
    }

    public Vertex GetRandomVertex()
    {
        int index = Random.Range(0, vertices.Length);
        return vertices[index];
    }

    public Path ShortestPath(Vertex from, Vertex to)
    {
        return new PathFinder(vertices, from, to).FindDefaultPath();
    }
}

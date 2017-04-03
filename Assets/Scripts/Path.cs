using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path {
    private List<Vertex> vertices;
    private int index = 0;

    public Path(List<Vertex> list)
    {
        vertices = list;
    }

    public Vertex GetCurrentVertex()
    {
        return vertices[index];
    }

    public Transform GetCurrentTarget()
    {
        return GetCurrentVertex().GetIntersection().transform;
    }

    public List<Vertex> GetVertices()
    {
        return vertices;
    }

    public bool GetNextTarget()
    {
        index++;

        return index < vertices.Count;
    }
}

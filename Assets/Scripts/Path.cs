using System.Collections.Generic;
using UnityEngine;

public class Path {
    private List<Vector3> waypoints = new List<Vector3>();
    private List<Vertex> vertices;
    private bool withPrepend = false;

    public Path(List<Vertex> vertices)
    {
        this.vertices = vertices;
    }

    public void Prepend(Vertex element)
    {
        withPrepend = true;
        vertices.Insert(0, element);
    }

    public void Calculate()
    {
        int index = 0;
        int count = vertices.Count;
        Direction turn;

        for (index = 0; index < count - 1; index++)
        {
            try
            {
                turn = Turn.GetDirection(vertices[index - 1], vertices[index], vertices[index + 1]);
            }
            catch
            {
                turn = Direction.FORWARD;
            }

            if (!withPrepend || index > 0)
            {
                waypoints.AddRange(new Segment(vertices[index], vertices[index + 1], turn).GetPath());
            }
        }
    }

    public Vertex GetVertex(int n)
    {
        if (withPrepend)
        {
            return vertices[n + 1];
        }

        return vertices[n];
    }

    public Vector3[] GetPath()
    {
        return waypoints.ToArray();
    }
}

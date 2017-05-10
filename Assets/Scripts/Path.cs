using System.Collections.Generic;
using UnityEngine;

public class Path {
    private List<Vector3> waypoints = new List<Vector3>();
    public Vertex finishPosition;

    public Path(List<Vertex> vertices)
    {
        int index = 0;
        int count = vertices.Count;
        finishPosition = vertices[count - 1];
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

            waypoints.AddRange(new Segment(vertices[index], vertices[index + 1], turn).GetPath());
        }
    }

    public Vector3[] GetPath()
    {
        return waypoints.ToArray();
    }
}

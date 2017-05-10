using System.Collections.Generic;
using UnityEngine;

public class Segment {
    private List<Vector3> waypoints = new List<Vector3>();
    private Direction turn;
    private Vector3 up = new Vector3(0, 1);

    public Segment(Vertex from, Vertex to, Direction turn)
    {
        Vector3 a = from.transform.position + up;
        Vector3 b = to.transform.position + up;
        float mainOffset = 10f;
        float sideOffset = 2.5f;
        this.turn = turn;

        if (Mathf.Approximately(a.z, b.z))
        {
            if (a.x > b.x)
            {
                mainOffset = -mainOffset;
            }
            else
            {
                sideOffset = -sideOffset;
            }

            AddWaypoints(a, b, new Vector3(mainOffset, 0, 0), new Vector3(0, 0, sideOffset));
        }
        else
        {
            if (a.z > b.z)
            {
                mainOffset = -mainOffset;
                sideOffset = -sideOffset;
            }

            AddWaypoints(a, b, new Vector3(0, 0, mainOffset), new Vector3(sideOffset, 0, 0));
        }
    }

    private void AddWaypoints(Vector3 a, Vector3 b, Vector3 mainOffset, Vector3 sideOffset)
    {
        if (turn == Direction.LEFT)
        {
            waypoints.Add(a);
            waypoints.Add(a + mainOffset + sideOffset * 0.5f);
        }
        else if (turn == Direction.RIGHT)
        {
            waypoints.Add(a + mainOffset + sideOffset);
        }

        waypoints.Add(a + mainOffset * 5 + sideOffset);
        waypoints.Add(b - mainOffset + sideOffset);
    }

    public List<Vector3> GetPath()
    {
        return waypoints;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Segment {
    private List<Vector3> waypoints = new List<Vector3>();

    public Segment(Vertex from, Vertex to)
    {
        Vector3 a = from.transform.position;
        Vector3 b = to.transform.position;
        float mainOffset = 10f;
        float sideOffset = 2f;

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
        }
        else if (a.z > b.z)
        {
            mainOffset = -mainOffset;
            sideOffset = -sideOffset;
        }

        this.AddWaypoints(waypoints, a, b, new Vector3(0, 0, mainOffset), new Vector3(sideOffset, 0, 0));
    }

    private void AddWaypoints(List<Vector3> waypoints, Vector3 a, Vector3 b, Vector3 mainOffset, Vector3 sideOffset)
    {
        Vector3 stabilizationOffset = mainOffset * 3f;
        waypoints.Add(a + mainOffset + sideOffset);
        waypoints.Add(a + stabilizationOffset + sideOffset);
        waypoints.Add(b - stabilizationOffset + sideOffset);
        waypoints.Add(b - mainOffset + sideOffset);
    }

    public List<Vector3> GetPath()
    {
        return waypoints;
    }
}

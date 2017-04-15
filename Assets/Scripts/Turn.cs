using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn {
    private List<Vector3> waypoints = new List<Vector3>();

    public Turn(Vertex from, Vertex via, Vertex to)
    {
        Vector3 a = from.transform.position;
        Vector3 b = via.transform.position;
        Vector3 c = to.transform.position;

        float direction = this.direction(a, b, c);

        if(Mathf.Approximately(0f, direction))
        {
            // do nothing
        }
        else if(direction > 0)
        {
            // turn left
        }
        else
        {
            // turn right
        }
    }

    private float direction(Vector3 a, Vector3 b, Vector3 c)
    {
        return ((b.x - a.x) * (c.z - a.z) - (b.z - a.z) * (c.x - a.x));
    }

    public List<Vector3> GetPath()
    {
        return waypoints;
    }
}

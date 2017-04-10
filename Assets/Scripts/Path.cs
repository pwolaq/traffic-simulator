using System.Collections.Generic;
using UnityEngine;

public class Path {
    private List<Vertex> vertices;
    private List<Vector3> waypoints;
    private IEnumerator<List<Vector3>> iterator;
    private int index = 0;

    public IEnumerator<List<Vector3>> PathIterator()
    {
        for(int index = 0, count = vertices.Count; index < count - 1; index++)
        {
            yield return Path.Segment(vertices[index], vertices[index + 1]);
        }
        // go straight from vertices[index] to vertices[index+1]
        // index++
        // make a turn
    }

    public static List<Vector3> Segment(Vertex from, Vertex to)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector3 a = from.transform.position;
        Vector3 b = to.transform.position;
        float mainOffset = 10f;
        float sideOffset = 2f;

        if(Mathf.Approximately(a.x, b.x))
        {
            if(a.z > b.z)
            {
                AddSegmentWaypoints(waypoints, a, b, new Vector3(0, 0, -mainOffset), new Vector3(-sideOffset, 0, 0));
            }
            else
            {
                AddSegmentWaypoints(waypoints, a, b, new Vector3(0, 0, mainOffset), new Vector3(sideOffset, 0, 0));
            }
        }
        else
        {
            if (a.x > b.x)
            {
                AddSegmentWaypoints(waypoints, a, b, new Vector3(-mainOffset, 0, 0), new Vector3(0, 0, sideOffset));
            }
            else
            {
                AddSegmentWaypoints(waypoints, a, b, new Vector3(mainOffset, 0, 0), new Vector3(0, 0, -sideOffset));
            }
        }
        
        return waypoints;
    }

    private static void AddSegmentWaypoints(List<Vector3> waypoints, Vector3 a, Vector3 b, Vector3 mainOffset, Vector3 sideOffset)
    {
        Vector3 stabilizationOffset = mainOffset * 3f;
        waypoints.Add(a + mainOffset + sideOffset);
        waypoints.Add(a + stabilizationOffset + sideOffset);
        waypoints.Add(b - stabilizationOffset + sideOffset);
        waypoints.Add(b - mainOffset + sideOffset);
    }

    public Path(List<Vertex> list)
    {
        vertices = list;
        iterator = PathIterator();
        iterator.MoveNext();
        waypoints = iterator.Current;
    }

    public bool GetNext()
    {
        index++;

        if(index >= waypoints.Count)
        {
            if (!iterator.MoveNext())
            {
                return false;
            }

            index = 0;
            waypoints = iterator.Current;
        }

        return true;
    }

    public Vector3 GetCurrentTarget()
    {
        return waypoints[index];
    }

    public List<Vertex> GetVertices()
    {
        return vertices;
    }
}

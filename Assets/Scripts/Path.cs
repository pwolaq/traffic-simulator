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
        Vector3 mainOffset;
        Vector3 sideOffset;
        Vector3 stabilizationOffset;

        if(Mathf.Approximately(a.x, b.x))
        {
            mainOffset = new Vector3(0, 0, 15f);
            sideOffset = new Vector3(2f, 0, 0);
            stabilizationOffset = mainOffset * 1.5f;

            if(a.z > b.z)
            {
                waypoints.Add(a - mainOffset - sideOffset);
                waypoints.Add(a - mainOffset - stabilizationOffset - sideOffset);
                waypoints.Add(b + mainOffset + stabilizationOffset - sideOffset);
                waypoints.Add(b + mainOffset - sideOffset);
            } else
            {
                waypoints.Add(a + mainOffset + sideOffset);
                waypoints.Add(a + mainOffset + stabilizationOffset + sideOffset);
                waypoints.Add(b - mainOffset - stabilizationOffset + sideOffset);
                waypoints.Add(b - mainOffset + sideOffset);
            }
        } else
        {
            mainOffset = new Vector3(15f, 0, 0);
            sideOffset = new Vector3(0, 0, 2f);
            stabilizationOffset = mainOffset * 1.5f;

            if (a.x > b.x)
            {
                waypoints.Add(a - mainOffset + sideOffset);
                waypoints.Add(a - mainOffset - stabilizationOffset + sideOffset);
                waypoints.Add(b + mainOffset + stabilizationOffset + sideOffset);
                waypoints.Add(b + mainOffset + sideOffset);
            }
            else
            {
                waypoints.Add(a + mainOffset - sideOffset);
                waypoints.Add(a + mainOffset + stabilizationOffset - sideOffset);
                waypoints.Add(b - mainOffset - stabilizationOffset - sideOffset);
                waypoints.Add(b - mainOffset - sideOffset);
            }
        }
        
        return waypoints;
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

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
        Vector3 offset;

        if(Mathf.Approximately(a.x, b.x))
        {
            offset = new Vector3(0, 0, 15f);

            if(a.z > b.z)
            {
                waypoints.Add(a - offset);
                waypoints.Add(b + offset);
            } else
            {
                waypoints.Add(a + offset);
                waypoints.Add(b - offset);
            }
        } else
        {
            offset = new Vector3(15f, 0, 0);

            if (a.x > b.x)
            {
                waypoints.Add(a - offset);
                waypoints.Add(b + offset);
            }
            else
            {
                waypoints.Add(a + offset);
                waypoints.Add(b - offset);
            }
        }

        Debug.Log(waypoints[0]);
        Debug.Log(waypoints[1]);
        
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

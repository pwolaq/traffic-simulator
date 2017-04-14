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
            yield return new Segment(vertices[index], vertices[index + 1]).GetPath();
        }
        // go straight from vertices[index] to vertices[index+1]
        // index++
        // make a turn
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

using System.Collections.Generic;
using UnityEngine;

public class Path {
    private List<Vertex> vertices;
    private List<Vector3> waypoints;
    private IEnumerator<List<Vector3>> iterator;
    private int index = 0;

    public IEnumerator<List<Vector3>> PathIterator()
    {
        int index = 0;
        int count = vertices.Count;
        List<Vector3> turn;
        yield return new Segment(vertices[index], vertices[index + 1]).GetPath();

        for (index = 1; index < count - 1; index++)
        {
            turn = new Turn(vertices[index - 1], vertices[index], vertices[index + 1]).GetPath();

            if(turn.Count > 0)
            {
                yield return turn;
            }

            yield return new Segment(vertices[index], vertices[index + 1]).GetPath();
        }
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

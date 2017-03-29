using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge {
    private Segment segment;
    private Vertex a;
    private Vertex b;

    public Edge(Vertex vA, Vertex vB, Segment s)
    {
        segment = s;
        a = vA;
        b = vB;

        a.AddEdge(this);
        b.AddEdge(this);
    }

    public Vertex GetA()
    {
        return a;
    }

    public Vertex GetB()
    {
        return b;
    }

    public Vertex GetNeighbor(Vertex from)
    {
        if (a.Equals(from))
        {
            return b;
        }
        else
        {
            return a;
        }
    }

    public int GetDistance()
    {
        return 1;
    }
}

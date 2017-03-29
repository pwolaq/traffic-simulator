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

    public Vertex getA()
    {
        return a;
    }

    public Vertex getB()
    {
        return b;
    }
}

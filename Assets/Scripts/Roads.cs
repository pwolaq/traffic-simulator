using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roads : MonoBehaviour {
    private Graph g;

	void Awake () {
        GameObject[] vertices = GameObject.FindGameObjectsWithTag("Intersection");
        GameObject[] edges = GameObject.FindGameObjectsWithTag("Segment");

        g = new Graph(vertices, edges);
	}

    public Vertex GetRandomIntersection()
    {
        return g.GetRandomVertex();
    }

    public Path GetPathToRandomTarget(Vertex from)
    {
        Vertex to = null;

        do
        {
            to = g.GetRandomVertex();
        } while (from.Equals(to));

        return g.ShortestPath(from, to);
    }
}

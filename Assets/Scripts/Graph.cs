using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph {
    private Vertex[] vertices;
    private int prevIndex = -1;

    public Graph(Vertex[] v)
    {
        vertices = v;
    }

    public Vertex GetRandomVertex()
    {
        int index;
        do
        {
            index = Random.Range(0, vertices.Length);
        } while (vertices[index].tag == "Respawn");
        return vertices[index];
    }

    public Vertex GetRandomRespawn()
    {
        GameObject[] respawns = GameObject.FindGameObjectsWithTag("Respawn");
        int index;
        do
        {
            index = Random.Range(0, respawns.Length);
        } while (index == prevIndex);

        prevIndex = index;
        return respawns[index].GetComponent<Vertex>();
    }

    public Path ShortestPath(Vertex from, Vertex to)
    {
        return new PathFinder(vertices, from, to).FindDefaultPath();
    }
}

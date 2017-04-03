using UnityEngine;

public class Roads : MonoBehaviour {
    private Graph g;

	void Awake () {
        g = new Graph(GetComponentsInChildren<Vertex>());
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

using UnityEngine;

public class Edge : MonoBehaviour
{
    private Vertex a;
    private Vertex b;
    private const float MARGIN = 50f;

    public void Start()
    {
        GameObject roads = GameObject.FindGameObjectWithTag("Roads");

        Vertex[] vertices = roads.GetComponentsInChildren<Vertex>();
        Bounds bounds = new Bounds(transform.position, Vector3.zero);

        foreach (Transform child in transform)
        {
            bounds.Encapsulate(child.gameObject.GetComponent<Renderer>().bounds);
        }

        Vector3 _a, _b;
        
        if(bounds.extents.x > bounds.extents.z)
        {
            _a = transform.position + new Vector3(bounds.extents.x / 2, 0, 0);
            _b = transform.position - new Vector3(bounds.extents.x / 2, 0, 0);
        }
        else
        {
            _a = transform.position + new Vector3(0, 0, bounds.extents.z / 2);
            _b = transform.position - new Vector3(0, 0, bounds.extents.z / 2);
        }

        foreach(Vertex vertex in vertices)
        {
            if(Vector3.Distance(vertex.transform.position, _a) < MARGIN)
            {
                a = vertex;
            }
            else if(Vector3.Distance(vertex.transform.position, _b) < MARGIN)
            {
                b = vertex;
            }
        }

        if(a != null && b != null)
        {
            a.AddEdge(this);
            b.AddEdge(this);
        }
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

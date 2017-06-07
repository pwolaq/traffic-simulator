using UnityEngine;

public class Edge : MonoBehaviour
{
    private Vertex vertexA;
    private Vertex vertexB;
    private Vector3 positionA;
    private Vector3 positionB;
    private int length = 0;

    private const float MARGIN = 50f;

    public void Start()
    {
        GameObject roads = GameObject.FindGameObjectWithTag("Roads");

        Vertex[] vertices = roads.GetComponentsInChildren<Vertex>();
        Bounds bounds = new Bounds(transform.position, Vector3.zero);
        Vector3 offset;

        foreach (Transform child in transform)
        {
            bounds.Encapsulate(child.gameObject.GetComponent<Renderer>().bounds);
        }
        
        if(bounds.extents.x > bounds.extents.z)
        {
            offset = new Vector3(bounds.extents.x / 2 + 30, 0, 0);
            length = (int) bounds.extents.x + 30;
        }
        else
        {
            offset = new Vector3(0, 0, bounds.extents.z / 2 + 30);
            length = (int) bounds.extents.z + 30;
        }

        positionA = transform.position + offset;
        positionB = transform.position - offset;

        foreach (Vertex vertex in vertices)
        {
            if (Vector3.Distance(vertex.transform.position, positionA) < MARGIN)
            {
                vertexA = vertex;
            }
            else if(Vector3.Distance(vertex.transform.position, positionB) < MARGIN)
            {
                vertexB = vertex;
            }
        }

        if(vertexA != null && vertexB != null)
        {
            vertexA.AddEdge(this);
            vertexB.AddEdge(this);
        }
    }

    public Vertex GetVertexA()
    {
        return vertexA;
    }

    public Vertex GetVertexB()
    {
        return vertexB;
    }

    public Vector3 GetPositionA()
    {
        return positionA;
    }

    public Vector3 GetPositionB()
    {
        return positionB;
    }

    public Vertex GetNeighbor(Vertex from)
    {
        if (vertexA.Equals(from))
        {
            return vertexB;
        }
        else
        {
            return vertexA;
        }
    }

    public int GetDistance()
    {
        return length;
    }
}

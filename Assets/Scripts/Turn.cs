using UnityEngine;

public enum Direction
{
    FORWARD, LEFT, RIGHT
};

public class Turn {
    public static Direction GetDirection(Vertex from, Vertex via, Vertex to)
    {
        Vector3 a = from.transform.position;
        Vector3 b = via.transform.position;
        Vector3 c = to.transform.position;

        float direction = CalculateDirection(a, b, c);

        if(Mathf.Approximately(0f, direction))
        {
            return Direction.FORWARD;
        }
        else if(direction > 0)
        {
            return Direction.LEFT;
        }
        else
        {
            return Direction.RIGHT;
        }
    }

    private static float CalculateDirection(Vector3 a, Vector3 b, Vector3 c)
    {
        return ((b.x - a.x) * (c.z - a.z) - (b.z - a.z) * (c.x - a.x));
    }
}

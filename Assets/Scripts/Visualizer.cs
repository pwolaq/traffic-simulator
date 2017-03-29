using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour {
    private Color colorIntersections = Color.red;
    private Color colorSegments = Color.green;
    private float radius = 5f;

    void OnDrawGizmos()
    {
        drawIntersections();
        drawSegments();
    }

    void drawIntersections()
    {
        Gizmos.color = colorIntersections;

        GameObject[] intersections = GameObject.FindGameObjectsWithTag("Intersection");

        foreach (GameObject intersection in intersections)
        {
            Transform transform = intersection.GetComponent<Transform>();
            Vector3 position = transform.position;
            Gizmos.DrawWireSphere(position, radius);
        }
    }

    void drawSegments()
    {
        Gizmos.color = colorSegments;

        GameObject[] segments = GameObject.FindGameObjectsWithTag("Segment");

        foreach (GameObject segment in segments)
        {
            Segment s = segment.GetComponent<Segment>();
          
            if (s.intersectionA != null && s.intersectionB != null)
            {
                Vector3 positionA = s.intersectionA.GetComponent<Transform>().position;
                Vector3 positionB = s.intersectionB.GetComponent<Transform>().position;

                positionA.y += 1;
                positionB.y += 1;

                Gizmos.DrawLine(positionA, positionB);
            }
        }
    }
}

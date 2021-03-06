﻿using UnityEngine;

public class Visualizer : MonoBehaviour {
    private Color colorIntersections = Color.red;
    private Color colorSegments = Color.green;
    private float radius = 3f;

    void OnDrawGizmosSelected()
    {
        drawIntersections();
        drawSegments();
    }

    void drawIntersections()
    {
        Gizmos.color = colorIntersections;

        foreach (Vertex v in GetComponentsInChildren<Vertex>())
        {
            Vector3 position = v.transform.position;
            Gizmos.DrawWireSphere(position, radius);
        }
    }

    void drawSegments()
    {
        Gizmos.color = colorSegments;

        foreach (Edge e in GetComponentsInChildren<Edge>())
        {
            if (e.GetVertexA() != null && e.GetVertexB() != null)
            {
                Vector3 positionA = e.GetVertexA().transform.position;
                Vector3 positionB = e.GetVertexB().transform.position;

                positionA.y += 1;
                positionB.y += 1;

                Gizmos.DrawLine(positionA, positionB);
            }
        }
    }
}

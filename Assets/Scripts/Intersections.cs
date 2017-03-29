using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersections : MonoBehaviour {
    private Color color = Color.red;
    private float radius = 5f;

    void OnDrawGizmos()
    {
        Gizmos.color = color;

        Transform[] intersections = GetComponentsInChildren<Transform>();

        foreach (Transform intersection in intersections)
        {
            Vector3 position = intersection.position;
            Gizmos.DrawWireSphere(position, radius);
        }
    }
}

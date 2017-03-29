using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour {
    public Transform intersectionA;
    public Transform intersectionB;

    private Color color = Color.green;

    void OnDrawGizmos()
    {
        Gizmos.color = color;

        if(intersectionA != null && intersectionB != null)
        {
            Vector3 positionA = intersectionA.position;
            Vector3 positionB = intersectionB.position;

            positionA.y += 1;
            positionB.y += 1;

            Gizmos.DrawLine(positionA, positionB);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roads : MonoBehaviour {
	void Start () {
        GameObject[] vertices = GameObject.FindGameObjectsWithTag("Intersection");
        GameObject[] edges = GameObject.FindGameObjectsWithTag("Segment");

        new Graph(vertices, edges);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roads : MonoBehaviour {
    private Graph graph = null;

	void Start () {
        GameObject[] vertices = GameObject.FindGameObjectsWithTag("Intersection");
        GameObject[] edges = GameObject.FindGameObjectsWithTag("Segment");

        graph = new Graph(vertices, edges);
	}
}

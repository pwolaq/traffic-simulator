using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles : MonoBehaviour {
    public Roads roads;
    public GameObject car;

    void Start()
    {
        Intersection i = roads.GetRandomIntersection();
        GameObject obj = Instantiate(car, i.transform.position, Quaternion.identity);
        obj.transform.parent = transform;
    }
}

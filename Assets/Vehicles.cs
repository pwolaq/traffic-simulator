using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles : MonoBehaviour {
    public Roads roads;
    public GameObject car;

    void Start()
    {
        AddCar();
        AddCar();
        AddCar();
    }

    private void AddCar()
    {
        Vertex vertex = roads.GetRandomIntersection();
        GameObject obj = Instantiate(car, vertex.GetIntersection().transform.position, Quaternion.identity);
        obj.transform.parent = transform;
        CarController ctrl = obj.GetComponent<CarController>();
        ctrl.position = vertex;
        ctrl.roads = roads;
        ctrl.SelectDestination();
    }
}

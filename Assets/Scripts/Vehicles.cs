using System.Collections;
using UnityEngine;

public class Vehicles : MonoBehaviour {
    public Roads roads;
    public GameObject car;

    private const float INTERVAL = 1f;
    private const int MAX_CARS = 3;

    private int count = 0;

    void Start()
    {
        StartCoroutine(AddVehicles());
    }

    IEnumerator AddVehicles()
    {
        while (count < MAX_CARS)
        {
            AddCar();
            yield return new WaitForSeconds(INTERVAL + Random.Range(0, INTERVAL));
        }
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
        count++;
    }
}

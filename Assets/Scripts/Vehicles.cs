using System.Collections;
using UnityEngine;

public class Vehicles : MonoBehaviour {
    public Roads roads;
    public GameObject car;

    private const float INTERVAL = 5f;
    private const int MAX_CARS = 5;

    private int count = 0;

    void Start()
    {
        StartCoroutine(AddVehicles());
    }

    IEnumerator AddVehicles()
    {
        while (count < MAX_CARS)
        {
            yield return new WaitForSeconds(INTERVAL + Random.Range(0, INTERVAL));
            AddCar();
        }
    }

    private void AddCar()
    {
        Vertex vertex = roads.GetRandomRespawn();
        GameObject obj = Instantiate(car, vertex.transform.position, Quaternion.identity);
        obj.transform.parent = transform;
        CarController ctrl = obj.GetComponent<CarController>();
        ctrl.position = vertex;
        ctrl.roads = roads;
        ctrl.SelectDestination();
        count++;
    }
}

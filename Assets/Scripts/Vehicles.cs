using System.Collections;
using UnityEngine;

public class Vehicles : MonoBehaviour {
    public Roads roads;
    public GameObject car;
    public UI ui;
    private Vertex last = null;

    private const float INTERVAL = 2f;
    private int maxCars = 0;
    private int count = 0;

    public void StartSimulation(int cars)
    {
        maxCars = cars;
        StartCoroutine(AddVehicles());
    }

    IEnumerator AddVehicles()
    {
        while (count < maxCars)
        {
            yield return new WaitForSeconds(INTERVAL + Random.Range(0, INTERVAL));
            AddCar();
        }
    }

    private void AddCar()
    {
        Vertex vertex;
        do {
            vertex = roads.GetRandomRespawn();
        }
        while (vertex == last);
        last = vertex;
        GameObject obj = Instantiate(car, vertex.transform.position, Quaternion.identity);
        obj.transform.parent = transform;
        CarMainController ctrl = obj.GetComponent<CarMainController>();
        ctrl.Setup(vertex, roads);
        count++;
        ui.SetCars(count);
    }
}

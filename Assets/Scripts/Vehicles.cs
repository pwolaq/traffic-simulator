using System.Collections;
using UnityEngine;

public class Vehicles : MonoBehaviour {
    public Roads roads;
    public GameObject car;
    public UI ui;

    private const float INTERVAL = 1f;
    private int maxCars = 0;
    private int count = 0;
    private float time = 0f;

    void Update()
    {
        time += Time.deltaTime;
        ui.SetTime((int) time);
    }

    public void StartSimulation(int cars)
    {
        gameObject.SetActive(true);
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
        Vertex vertex = roads.GetRandomRespawn();
        GameObject obj = Instantiate(car, vertex.transform.position, Quaternion.identity);
        obj.transform.parent = transform;
        CarMainController ctrl = obj.GetComponent<CarMainController>();
        ctrl.Setup(vertex, roads);
        count++;
        ui.SetCars(count);
    }
}

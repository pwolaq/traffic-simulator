using System.Collections;
using UnityEngine;

public class Vehicles : MonoBehaviour {
    public Roads roads;
    public GameObject car;

    private const float INTERVAL = 5f;
    private const int MAX_CARS = 15;

    private int count = 0;

    void Start()
    {
        //StartCoroutine(AddVehicles());
        AddCar();
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
        CarMainController ctrl = obj.GetComponent<CarMainController>();
        ctrl.Setup(vertex, roads);
        count++;
    }
}

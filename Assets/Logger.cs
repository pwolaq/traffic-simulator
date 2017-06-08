using UnityEngine;

public class Logger : MonoBehaviour {
    private float delta = 0;
    private float deltaMax = 1f;
    private int time = 0;
    private System.IO.StreamWriter file;

    void Awake()
    {
        System.DateTime now = System.DateTime.Now;
        file = new System.IO.StreamWriter("c:\\" + now.Year + LeadingZero(now.Month) + LeadingZero(now.Day) + LeadingZero(now.Hour) + LeadingZero(now.Minute) + ".txt");
    }

    string LeadingZero(int val)
    {
        return val < 10 ? "0" + val : val.ToString();
    }

    void Update () {
        delta += Time.deltaTime;

        if (delta > deltaMax)
        {
            delta = 0;
            time++;
            CarMainController[] cars = GetComponentsInChildren<CarMainController>();
            float total = 0;

            foreach (CarMainController car in cars)
            {
                total += car.GetDistance();
            }

            file.WriteLine(time + ";" + cars.Length + ";" + total);
        }
    }

    void OnApplicationQuit()
    {
        file.Close();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public Text carsText;
    public Slider carsSlider;
    public Text intervalText;
    public Slider intervalSlider;

    public Text maxCarsText;
    public Text currentCarsText;
    public Text time;

    public Vehicles controller;

    private int cars;
    private int interval;
    
	void Start () {
        OnCarSliderChanged();
        OnIntervalSliderChanged();
	}

    public void StartSimulation()
    {
        maxCarsText.text = cars.ToString();
        controller.StartSimulation(cars, interval);
    }

    public void SetCars(int n)
    {
        currentCarsText.text = n.ToString();
    }

    public void SetTime(int passed)
    {
        int minutes = passed / 60;
        int seconds = passed % 60;
        time.text = minutes.ToString() + ":" + (seconds >= 10 ? seconds.ToString() : "0" + seconds.ToString());
    }

    public void OnCarSliderChanged()
    {
        cars = (int) carsSlider.value;
        carsText.text = cars.ToString();
    }

    public void OnIntervalSliderChanged()
    {
        interval = (int)intervalSlider.value;
        intervalText.text = interval.ToString();
    }
}

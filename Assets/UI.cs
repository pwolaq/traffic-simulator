using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public Text carsText;
    public Slider carsSlider;

    public Text maxCarsText;
    public Text currentCarsText;
    public Text time;

    public Vehicles controller;

    private int cars;
    
	void Start () {
        OnSliderChanged();
	}

    public void StartSimulation()
    {
        maxCarsText.text = cars.ToString();
        controller.StartSimulation(cars);

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

    public void OnSliderChanged()
    {
        cars = (int) carsSlider.value;
        carsText.text = cars.ToString();
    }
}

using System.Collections;
using UnityEngine;

public class TrafficLights : MonoBehaviour {
    public Light red;
    public Light yellow;
    public Light green;
    
    public static float CHANGE_DELAY = 1f;
    private const float INTENSITY_ON = 8f;
    private const float INTENSITY_OFF = 0.5f;

    void Awake()
    {
        TurnOff(red);
        TurnOff(green);
        TurnOff(yellow);
    }

    public void SetGreen()
    {
        StartCoroutine(_SetGreen());
    }

    public void SetRed()
    {
        StartCoroutine(_SetRed());
    }

    private IEnumerator _SetGreen()
    {
        yield return new WaitForSeconds(CHANGE_DELAY * 2);
        TurnOn(yellow);
        yield return new WaitForSeconds(CHANGE_DELAY);
        TurnOn(green);
        TurnOff(yellow);
        TurnOff(red);
    }

    private IEnumerator _SetRed()
    {
        TurnOff(green);
        TurnOn(yellow);
        yield return new WaitForSeconds(CHANGE_DELAY);
        TurnOff(yellow);
        TurnOn(red);
    }

    private void TurnOn(Light light)
    {
        light.intensity = INTENSITY_ON;
    }

    private void TurnOff(Light light)
    {
        light.intensity = INTENSITY_OFF;
    }
}

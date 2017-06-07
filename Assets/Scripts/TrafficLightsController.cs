using System.Collections;
using UnityEngine;

public class TrafficLightsController : MonoBehaviour {
    public enum Direction { A, B, CHANGING };
    public static float intervalA;
    public static float intervalB;
    public TrafficLights[] lightsA = new TrafficLights[2];
    public TrafficLights[] lightsB = new TrafficLights[2];
    public Direction current = Direction.A;

    void Start()
    {
        StartCoroutine(ChangeLights());
    }

    public bool CanGo(Vector3 position, bool veryClose)
    {
        if (veryClose && current == Direction.CHANGING)
        {
            return true;
        }

        Vector3 diff = position - transform.position;

        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.z))
        {
            return current == Direction.A;
        }
        else
        {
            return current == Direction.B;
        }

        return false;
    }

    IEnumerator ChangeLights()
    {
        while (true)
        {
            if (current == Direction.A)
            {
                current = Direction.CHANGING;
                TurnOff(lightsB);
                yield return new WaitForSeconds(TrafficLights.CHANGE_DELAY);
                TurnOn(lightsA);
                yield return new WaitForSeconds(TrafficLights.CHANGE_DELAY * 3);
                current = Direction.B;

                yield return new WaitForSeconds(intervalA);
            }
            else
            {
                current = Direction.CHANGING;
                TurnOff(lightsA);
                yield return new WaitForSeconds(TrafficLights.CHANGE_DELAY);
                TurnOn(lightsB);
                yield return new WaitForSeconds(TrafficLights.CHANGE_DELAY * 3);
                current = Direction.A;

                yield return new WaitForSeconds(intervalB);
            }
        }
    }

    private void TurnOn(TrafficLights[] lights)
    {
        lights[0].SetGreen();
        lights[1].SetGreen();
    }

    private void TurnOff(TrafficLights[] lights)
    {
        lights[0].SetRed();
        lights[1].SetRed();
    }
}

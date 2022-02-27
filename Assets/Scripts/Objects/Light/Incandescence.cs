using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Incandescence : MonoBehaviour
{
    public Light2D light;

    private bool isClose = false;

    private readonly float maxRadius = 60f;
    private readonly float maxIntensity = 1.5f;

    private void Update()
    {
        if (Input.GetButtonDown("Action") && isClose)
        {
            light.pointLightOuterRadius += 5;
            light.intensity += 0.1f;
        }

        if (light.pointLightOuterRadius >= maxRadius && light.intensity >= maxIntensity)
        {
            Reality.allLightsTurnedOn = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isClose = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isClose = false;
    }
}

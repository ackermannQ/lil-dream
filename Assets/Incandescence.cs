using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Incandescence : MonoBehaviour
{
    private float radius;
    private float initRadius;
    void Start()
    {
        initRadius = GetComponent<Light2D>().pointLightInnerRadius;
        Debug.Log(initRadius);
    }

    private void OnTriggerEnter2d(Collider2D col)
    {
        radius = initRadius + 15f;
        Debug.Log("Entered");
        Debug.Log(radius);
    }

    private void OnTriggerExit2d(Collider2D col)
    {
        Debug.Log("Exited");
        Debug.Log(radius);
        radius += initRadius;
    }
}

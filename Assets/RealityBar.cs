using UnityEngine;
using UnityEngine.UI;

public class RealityBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxRealityPoint(int maxRealityPoints)
    {
        slider.maxValue = maxRealityPoints;
        slider.minValue = 0;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetRealityPoints(int realityPoints)
    {
        slider.value = realityPoints;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

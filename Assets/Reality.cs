using UnityEngine;

public class Reality : MonoBehaviour
{
    public int MaxRealityPoints = 5;
    public int CurrentRealityPoints;

    public RealityBar realityBar;
    void Start()
    {
        CurrentRealityPoints = MaxRealityPoints;
        realityBar.SetMaxRealityPoint(MaxRealityPoints);
    }


    void Update()
    {
        // if hasBeenCatched
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoseReality();
        }

        // If !hasBeenCatch and allLightsTurnedOn
        if (Input.GetKeyDown(KeyCode.E))
        {
            GainReality();
        }
    }

    private void LoseReality()
    {
        CurrentRealityPoints -= 1;

        if (CurrentRealityPoints <= 0)
        {
            CurrentRealityPoints = 0;
        }

        realityBar.SetRealityPoints(CurrentRealityPoints);
    }

    private void GainReality()
    {
        CurrentRealityPoints += 1;

        if (CurrentRealityPoints >= MaxRealityPoints)
        {
            CurrentRealityPoints = MaxRealityPoints;
        }

        realityBar.SetRealityPoints(CurrentRealityPoints);
    }
}

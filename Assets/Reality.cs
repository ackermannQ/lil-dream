using UnityEngine;
using UnityEngine.SceneManagement;

public class Reality : MonoBehaviour
{
    public int MaxRealityPoints = 5;
    public int CurrentRealityPoints;

    public RealityBar realityBar;

    public static bool hasBeenCatch = false;
    public static bool allLightsTurnedOn = false;

    private bool loadScene = false;

    void Start()
    {
        CurrentRealityPoints = MaxRealityPoints;
        realityBar.SetMaxRealityPoint(MaxRealityPoints);
    }


    void Update()
    {
        if (hasBeenCatch)
        {
            LoseReality();
        }

        if (!hasBeenCatch && allLightsTurnedOn)
        {
            GainReality();
            loadScene = true;
        }

        if (loadScene)
        {
            LoadMainScene();
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

    private void LoadMainScene()
    {
        loadScene = false;
        allLightsTurnedOn = false;
        SceneManager.LoadScene(1);
    }
}

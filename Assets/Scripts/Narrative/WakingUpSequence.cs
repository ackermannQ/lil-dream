using System.Collections;
using TMPro;
using UnityEngine;

public class WakingUpSequence : MonoBehaviour
{
    public TextMeshProUGUI ThinkingText;

    public TextMeshProUGUI Indications;

    public GameObject SpeechBubble;
    public GameObject MovingAround;
    void Start()
    {
        StartCoroutine(WakingUp());
    }

    IEnumerator WakingUp()
    {
        yield return new WaitForSeconds(2f);
        SpeechBubble.SetActive(true);
        ThinkingText.text = "Ouuuh";
        yield return new WaitForSeconds(4f);
        ThinkingText.text = "That was some bad dreams ...";
        yield return new WaitForSeconds(3f);
        ThinkingText.text = "";
        SpeechBubble.SetActive(false);
        Indications.text = "Have a look around";
        yield return new WaitForSeconds(3f);
        Indications.text = "";
        MovingAround.SetActive(true);
        yield return new WaitForSeconds(5f);
        MovingAround.SetActive(false);
    }
}

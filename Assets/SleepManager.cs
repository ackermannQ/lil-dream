using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SleepManager : MonoBehaviour
{
    public Queue<string> Sentences;

    public Text dialogText;

    public Animator anim;
    void Start()
    {
        Sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        anim.SetBool("IsOpen", true);
        Debug.Log(anim.GetBool("IsOpen"));

        Sentences.Clear();

        foreach (var sentence in dialog.sentences)
        {
            Sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Debug.Log(anim.GetBool("IsOpen"));

        if (Sentences.Count == 0)
        {
            EndOfDialog();

            return;
        }

        var sentence = Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";

        foreach (var letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void EndOfDialog()
    {
        anim.SetBool("IsOpen", false);
    }

    public void GoToSleep()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

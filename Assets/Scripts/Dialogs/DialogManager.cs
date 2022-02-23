using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
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
        Sentences.Clear();

        foreach (var sentence in dialog.sentences)
        {
            Sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
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
}

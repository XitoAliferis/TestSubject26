using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialougeText;
    public Animator animator;

    private Queue<string> sentences;

    public GameObject ConversationTrigger;

    void Start()
    {
      
        sentences = new Queue<string>();



    }

    public void StartDialouge( Dialouge dialouge)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialouge.name;

        sentences.Clear();
        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialouge();
            animator.SetBool("close", true);
            return;
        }
        string sentence =sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialougeText.text = "";
        float typingSpeed = 0.03f;

        foreach (char letter in sentence.ToCharArray())
        {
            dialougeText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    void EndDialouge()
    {
        Debug.Log("End of conversation");
        animator.SetBool("IsOpen", false);

        if (ConversationTrigger != null)
        {
            ConversationTrigger.SetActive(false);
        }
    }


}

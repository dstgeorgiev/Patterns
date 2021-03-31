using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI dialogueTxt;
    public GameObject whoSpeaks;
    

    private Queue<string> sentences;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        nameTxt.text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        ShowNextSentence();
    }

    public void ShowNextSentence()
    {
        if(sentences.Count==0)
        {
            //the dialogue ended
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueTxt.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    
    IEnumerator TypeSentence (string sentence)
    {
        dialogueTxt.text = "";
        foreach (char character in sentence.ToCharArray())
        {
            dialogueTxt.text += character;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Time.timeScale = 1f;
        whoSpeaks.SetActive(false);
    }
}

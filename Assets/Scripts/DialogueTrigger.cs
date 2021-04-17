using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject whoSpeaks;
    public Dialogue dialogue;

    private bool wasTriggered = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&!wasTriggered)
        {
            
            TriggerDialogue();
            wasTriggered = true;
        }
    }

    public void TriggerDialogue()
    {
        whoSpeaks.SetActive(true);
        Time.timeScale = 0f;
        if(FindObjectOfType<DialogueManager>()!=null)
        {

            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        if (FindObjectOfType<DialogueFairyManager>() != null)
        {
            Debug.Log("Triggered");
            FindObjectOfType<DialogueFairyManager>().StartDialogue(dialogue);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

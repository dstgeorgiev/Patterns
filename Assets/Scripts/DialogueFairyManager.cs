using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueFairyManager : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI dialogueTxt;
    public GameObject whoSpeaks;
    public GameObject decisionButtons;
    public GameObject continueButton;
    public GameObject enchantedButton;
    public GameObject healthPotionCounter;

    public bool isFairyOfferingToKillGoblins = false;
    public bool isCastleLevel = false;
    private int sentencesPassed = 0;


    private Queue<string> sentences;

    private bool wasStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DFM started");
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        if(!wasStarted)
        {
            nameTxt.text = dialogue.name;
            sentences.Clear();
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            if(!isFairyOfferingToKillGoblins)
            {
                ShowNextSentence();
            }
            else
            {
                if(!isCastleLevel)
                {
                    SpecialShowNextSentence();
                }
                else
                {
                    SpecialTwoShowNextSentence();
                }
                

            }
            
        }
         
    }

    public void SpecialShowNextSentence()
    {
        sentencesPassed++;
        if (sentences.Count == 0)
        {
            //the dialogue ended
            MakeADecision();
            return;
        }
        string sentence = sentences.Dequeue();
        if(sentencesPassed==3)
        {
            string coinsPlayerHas = (FindObjectOfType<PlayerControl>().GetCoins() - 1).ToString();
            sentence = coinsPlayerHas +" "+ sentence;
            dialogueTxt.text = sentence;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            return;
        }

        
        dialogueTxt.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void SpecialTwoShowNextSentence()
    {
        sentencesPassed++;
        if (sentences.Count == 0)
        {
            //the dialogue ended
            MakeADecision();
            return;
        }
        string sentence = sentences.Dequeue();
        if (sentencesPassed == 3)
        {
            string coinsPlayerHas;
            if (FindObjectOfType<PlayerControl>().IsPlayerEnchanted())
            {
                coinsPlayerHas = (FindObjectOfType<PlayerControl>().GetCoins() + 2).ToString();
            }
            else
            {
                coinsPlayerHas = 1.ToString();
            }
            
            sentence = coinsPlayerHas + " " + sentence;
            dialogueTxt.text = sentence;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            return;
        }


        dialogueTxt.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void ShowNextSentence()
    {
        if (sentences.Count == 0)
        {
            //the dialogue ended
            MakeADecision();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueTxt.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueTxt.text = "";
        foreach (char character in sentence.ToCharArray())
        {
            dialogueTxt.text += character;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Time.timeScale = 1f;
        whoSpeaks.SetActive(false);
        wasStarted = true;
    }

    private void MakeADecision()
    {
        decisionButtons.SetActive(true);
        continueButton.SetActive(false);
    }

    public void EnchantPlayer()
    {
        FindObjectOfType<PlayerControl>().SetEnchanted(true);
    }

    public void RemoveEnchantmentPlayer()
    {
        FindObjectOfType<PlayerControl>().SetEnchanted(false);
    }

    public void SetEnchantedButtonActive()
    {
        enchantedButton.SetActive(true);
        FindObjectOfType<FairyMenu>().PublicStart();
    }

    public void MakeHealthPotionCOunterVisible()
    {
        healthPotionCounter.SetActive(true);
    }

    public void ChargePlayerForEnchantment()
    {
        FindObjectOfType<PlayerControl>().SetCoins(FindObjectOfType<PlayerControl>().GetCoins() - 1);

    }

    public void ChargePlayerForEnchantmentKill()
    {
        FindObjectOfType<PlayerControl>().SetCoins(1);

    }

    public void KillEnemy()
    {
        FindObjectOfType<EnemyControl>().Die();
    }

    public void ChargeAndKill()
    {
        
        if (!FindObjectOfType<PlayerControl>().IsPlayerEnchanted())
        {
            int coins = FindObjectOfType<PlayerControl>().GetCoins();
            if(coins>=1)
            {
                foreach(EnemyControl goblin in FindObjectsOfType<EnemyControl>())
                {
                    goblin.Die();
                }
                FindObjectOfType<PlayerControl>().SetCoins(FindObjectOfType<PlayerControl>().GetCoins() - 1);
                
            }
        }
        
    }

    public void SetPickMenuActive()
    {
        enchantedButton.SetActive(true);
    }

    public void EndGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}

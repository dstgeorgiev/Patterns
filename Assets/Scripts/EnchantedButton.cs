using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnchantedButton : MonoBehaviour
{
    public GameObject fairyMenyUI;
    public GameObject enchantedButtonUI;
    private bool isMenuActive = false;

    public void OpenOrCloseFairyMenu()
    {
        if(isMenuActive)
        {
            fairyMenyUI.SetActive(false);
            enchantedButtonUI.SetActive(true);
            Time.timeScale = 1f;
        }
        else
        {
            fairyMenyUI.SetActive(true);
            enchantedButtonUI.SetActive(false);
            Time.timeScale = 0f;
        }
        
    }
}

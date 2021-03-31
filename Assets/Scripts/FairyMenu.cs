using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FairyMenu : MonoBehaviour
{
    public GameObject fairyMenuUi;
    public GameObject enchantedButtonUi;

    public PlayerControl player;
    public TextMeshProUGUI potionPriceText;
    public TextMeshProUGUI numberOfPotionsPlayerHasText;

    private int numberOfPotionsPlayerHas;
    private int potionPrice = 5;

    private bool isPlayerEnchanted;
    private int playerCoins;

    private void Start()
    {
        Debug.Log("Fairy menu Started");
       // player.SetEnchanted(true);
        //player.SetCoins(30);
        isPlayerEnchanted = player.IsPlayerEnchanted();
        playerCoins = player.GetCoins();
        numberOfPotionsPlayerHas = int.Parse(numberOfPotionsPlayerHasText.text);
        Debug.Log("Number of potions player has: " + numberOfPotionsPlayerHas);
        EditPrice();
    }

    public void PublicStart()
    {
        isPlayerEnchanted = player.IsPlayerEnchanted();
        playerCoins = player.GetCoins();
        numberOfPotionsPlayerHas = int.Parse(numberOfPotionsPlayerHasText.text);

        EditPrice();
    }

    public void BuyPotions()
    {
        playerCoins = player.GetCoins();
        //buys potion if player has enough money
        if (playerCoins-potionPrice>=0)
        {
            numberOfPotionsPlayerHas = int.Parse(numberOfPotionsPlayerHasText.text);
            numberOfPotionsPlayerHas++;
            
            playerCoins = playerCoins - potionPrice;
            player.SetCoins(playerCoins);
            player.SetPotions(numberOfPotionsPlayerHas);
            numberOfPotionsPlayerHasText.text = numberOfPotionsPlayerHas.ToString();
        }
        else
        {
            Debug.Log("Not Enough Money!");
        }
    }

    public void GoToForest()
    {
        SceneManager.LoadScene("level10_FOREST");
    }

    public void Close()
    {
        fairyMenuUi.SetActive(false);
        Time.timeScale = 1f;
        enchantedButtonUi.SetActive(true);
    }

    /*if player has less than 5 coins the price
     * of the coins remains the same
     * else - the scripts makes the prices of the potions high enough
     * so the player can buy at most 3 potions with the current number
     * of coins he has
     */
    private void EditPrice()
    {
        if(isPlayerEnchanted)
        {
            if(playerCoins%3==0)
            {
                potionPrice = playerCoins / 3;
                if(potionPrice<5)
                {
                    potionPrice = 5;
                }
                potionPriceText.text = potionPrice.ToString();
            }
            if(playerCoins%3==1)
            {
                potionPrice = (playerCoins - 1) / 3;
                if (potionPrice < 5)
                {
                    potionPrice = 5;
                }
                potionPriceText.text = potionPrice.ToString();
            }
            if(playerCoins%3==2)
            {
                potionPrice = (playerCoins - 2) / 3;
                if (potionPrice < 5)
                {
                    potionPrice = 5;
                }
                potionPriceText.text = potionPrice.ToString();
            }
        }
        else
        {
            potionPriceText.text = potionPrice.ToString();
        }
    }
}

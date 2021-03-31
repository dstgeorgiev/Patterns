using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealCoins : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerControl>().SetCoins(0);
            FindObjectOfType<PlayerControl>().SetPotions(0);
            FindObjectOfType<PlayerControl>().SetPotionsLevel(0);
        }
    }
}

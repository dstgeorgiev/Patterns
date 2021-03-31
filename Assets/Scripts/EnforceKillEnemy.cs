using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnforceKillEnemy : MonoBehaviour
{
    public int goblinsToKill = 0;
    private int deadGoblins = 0;
    public GameObject wall;

    public void GoblinKilled()
    {
        deadGoblins++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            checkAllGoblinsKilled();
        }
    }

    public void checkAllGoblinsKilled()
    {
        if(deadGoblins>=goblinsToKill)
        {
            Destroy(wall);
        }
    }
}

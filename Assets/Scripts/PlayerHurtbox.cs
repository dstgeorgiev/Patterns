using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    //this method is called 
    public void TakeHit(int damage)
    {
        currentHealth -= damage;
        PlayerControl player = GetComponentInParent<PlayerControl>();

        player.GetHurt();
        if (currentHealth <= 0)
        {
            player.Die();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public PlayerHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHelth(maxHealth);
    }

    //this method is called 
    public void TakeHit(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealt(currentHealth);
        PlayerControl player = GetComponentInParent<PlayerControl>();


        player.GetHurt();
        if (currentHealth <= 0)
        {
            player.Die();
        }
    }
}

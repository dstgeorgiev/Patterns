using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public int maxHealth = 200;
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
        Debug.Log("current player health before attack: " + currentHealth);
        currentHealth = currentHealth - damage;
        Debug.Log("current player health after attack: " + currentHealth);
        healthBar.SetHealt(currentHealth);
        PlayerControl player = GetComponentInParent<PlayerControl>();


        player.GetHurt();
        if (currentHealth <= 0)
        {
            player.Die();
        }
    }
}

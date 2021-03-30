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

    // player gets hurt by the amount of dmg passed to the method
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

    //the player gets healed by the amount of health the potion gives
    public void Heal(int heal)
    {
        if(currentHealth+heal>maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.SetHealt(currentHealth);
        }
        else
        {
            currentHealth = currentHealth + heal;
            healthBar.SetHealt(currentHealth);
        }
        
    }
}

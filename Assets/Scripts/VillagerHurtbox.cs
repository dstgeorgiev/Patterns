using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerHurtbox : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealt(currentHealth, maxHealth);
    }

    //the enemy's life is updated repending on the dmg it takes
    public void TakeHit(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealt(currentHealth, maxHealth);
        VillagerControl villager = GetComponentInParent<VillagerControl>();

        //villager.GetHurt();
        if (currentHealth <= 0)
        {
        //    villager.Die();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
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
        EnemyControl enemy = GetComponentInParent<EnemyControl>();

        enemy.GetHurt();
        if (currentHealth<=0)
        {
            enemy.Die();
        }
    }
}

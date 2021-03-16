using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHurtbox : MonoBehaviour
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

        if (currentHealth <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            GetComponentInChildren<HealthBar>().slider.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerControl : MonoBehaviour
{
    public float speed = 1.0f;
    public float changeTime = 3.0f;

    private Rigidbody2D rb;
    private Animator animator;

    private float timer;
    public int direction { get; set; } = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }

        float xVelocity = direction * speed;
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Round(Mathf.Abs(direction)));
    }

    //the villager dies
    public void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponentInChildren<HealthBar>().slider.gameObject.SetActive(false);
        GetComponentInChildren<VillagerHurtbox>().enabled = false;


        Debug.Log("Goblin died!");
    }

    //plays hurt annimation
    public void GetHurt()
    {
        
    }
}

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed = 1.0f;
    public float changeTime = 3.0f;
   
    private Rigidbody2D rb;
    private Animator animator;

    private float timer;
    public int direction { get; set; } = 1;

    public Transform attackPoint1;
    public Transform attackPoint2;
    public LayerMask characterLayers;

    public int attackDmg = 15;
    public float attackRate = 2f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
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

    public void Attack()
    {
        animator.SetTrigger("Attacking");

        //detect enemies
        Collider2D[] charactersHit = Physics2D.OverlapAreaAll(attackPoint1.position, attackPoint2.position, characterLayers);

        //damage enemies
        foreach (Collider2D character in charactersHit)
        {
            character.GetComponent<PlayerHurtbox>().TakeHit(attackDmg);
            Debug.Log("Goblin hit:" + character.name);
        }
    }

    public void StopAttack()
    {
        animator.SetBool("Attacking", false);
    }

    //the enemy dies
    public void Die()
    {
        animator.SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponentInChildren<HealthBar>().slider.gameObject.SetActive(false);
        GetComponentInChildren<EnemyHurtbox>().enabled = false;
        FindObjectOfType<EnforceKillEnemy>().GoblinKilled();

        Debug.Log("Goblin died!");
    }

    //plays hurt annimation
    public void GetHurt()
    {
        animator.SetTrigger("Hurt");
    }

    public Animator GetAnimator()
    {
        return animator;
    }

}

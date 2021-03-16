using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.Rendering.LWRP;
//using UnityEngine.Experimental.Rendering.Universal;

public class PlayerControl : MonoBehaviour
{
    //public fields
    public float speed = 1;
    public float jumpPower = 200;

    //private fields
    Rigidbody2D rb;

    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    bool isJumping = false;
    
    const float groundCheckRadius = 0.2f;

    Animator animator;
    [SerializeField] bool isGrounded = false;
    float horizontalValue;
    bool faceRight = true;

    public Transform attackPoint1;
    public Transform attackPoint2;
    public LayerMask enemyLayers;

    public int attackDmg = 25;
    public float attackRate = 2f;
    float nextAtckTime = 0f;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        
        //when w is pressed, jump is enabled
        if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("Jumping", true);
            isJumping = true;
        }
        //otherwise it is disabled
        else if(Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }
        //set the yVelocity
        animator.SetFloat("yVelocity", rb.velocity.y);
        //when space is pressed - attack
        if(Time.time >= nextAtckTime)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                //setting the next time that we will be able to attack
                nextAtckTime = Time.time + 1f / attackRate;    
            }
        }
        
    }

    void GroundCheck()
    {
        isGrounded = false;
        /*check if the GroundCheckObject is colliding with
         * 2D Colliders that are in the "Ground" layer
         */
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(colliders.Length > 0)
        {
            isGrounded = true;
        }
        animator.SetBool("Jumping", !isGrounded);
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, isJumping);
    }

    //dir only gives values -1 and 1
    void Move(float dir, bool jumpCheck)
    {
        //if grounded and the Space button is pressed - JUMP
        if(isGrounded && jumpCheck)
        {
            //isGrounded = false;
            jumpCheck = false;
            rb.AddForce(new Vector2(0f, jumpPower));
        }

        #region Move
        //seting value of x using speed and dir
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        //Create Vec2 for the velocity 
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //set the player's velocity
        rb.velocity = targetVelocity;

      
        //when looking right and clicked left flip  left
        if(faceRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            faceRight = false;
        }
        //when looking left and clicked right flip right
        else if(!faceRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            faceRight = true;
        }
        animator.SetFloat("Player Speed", Mathf.Abs(dir*speed));
        #endregion
    }

    //player attacks
    void Attack()
    {
        //attack animation
        animator.SetTrigger("Attack2");

        //detect enemies
        Collider2D[] enemiesHit = Physics2D.OverlapAreaAll(attackPoint1.position, attackPoint2.position, enemyLayers);

        //damage enemies
        foreach(Collider2D enemy in enemiesHit)
        {
            if(enemy.GetComponent<EnemyHurtbox>()!=null)
            {
                enemy.GetComponent<EnemyHurtbox>().TakeHit(attackDmg);
            }
            else if(enemy.GetComponent<VillagerHurtbox>()!=null)
            {
                enemy.GetComponent<VillagerHurtbox>().TakeHit(attackDmg);
            }
            

            Debug.Log("we hit:" + enemy.name);
        }

    }

    //plays hurt animation
    public void GetHurt()
    {
        animator.SetTrigger("Hurt");
    }

    //the enemy dies
    public void Die()
    {
        animator.SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        Debug.Log("Player died!");
    }
}


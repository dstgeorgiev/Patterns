using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    //for the attack range
    public Transform attackPoint1;
    public Transform attackPoint2;
    public LayerMask enemyLayers;

    //for the attack 
    public int attackDmg = 25;
    public float attackRate = 2f;
    float nextAtckTime = 0f;

    //for the footstep sounds
    private const int FOOTSTEP_DELAY = 12;
    private int soundDelay = FOOTSTEP_DELAY;

    //for the coins counter
    private int coins;
    private int coinsThisLevel;
    public TextMeshProUGUI textCoinCounter;

    //for the potion counter
    private int potions;
    private int potionsThisLevel;
    public TextMeshProUGUI textPotionsCounter;
    public int potionHealsBy = 50;

    //for the fairy contract
    private bool isEnchanted = false;

    //for when dead
    public GameObject gameOverMenu;

    //for the killed villagers
    private int killedVillagers;
    private int killedVillagersThisLevel;

    //for the sound
    private int isGameOverSoundPlaying;

    //for the level remember
    private int indexLevel;

    private void Start()
    {
        isGameOverSoundPlaying = PlayerPrefs.GetInt("game_over", 0);
        if(isGameOverSoundPlaying==1)
        {
            FindObjectOfType<AudioManager>().StopPlay("game_over");
            isGameOverSoundPlaying = 0;
            PlayerPrefs.SetInt("game_over", 0);
        }        
        Debug.Log("Player Started");
        coins = PlayerPrefs.GetInt("coins", 0);
        coinsThisLevel = coins;
        Debug.Log("Coins start level set to: " + coinsThisLevel);
        potions = PlayerPrefs.GetInt("potions", 0);
        potionsThisLevel = potions;
        Debug.Log("Potions start level set to: " + potions);
        killedVillagers = PlayerPrefs.GetInt("villagers", 0);
        killedVillagersThisLevel = killedVillagers;
        if (PlayerPrefs.GetInt("isEnchanted", 0) == 1)
        {
            isEnchanted = true;
        }
        else
        {
            isEnchanted = false;
        }
        if (textCoinCounter != null)
        {
            Debug.Log("PlayerStart has set coins to: " + coins);
            textCoinCounter.text = coins.ToString();
        }
        if (textPotionsCounter != null)
        {
            Debug.Log("PlayerStart has set potions to: " + potions);
            textPotionsCounter.text = potions.ToString();
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        rb.freezeRotation = true;
    }

    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");

        //when p is pressed, the player drinks 1 health potion
        if(Input.GetKeyDown(KeyCode.P))
        {
            DrinkPotion();
        }
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
            FindObjectOfType<AudioManager>().Play("player_jump");
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
            Debug.Log("player is walking");
        }
        animator.SetFloat("Player Speed", Mathf.Abs(dir*speed));
        if (animator.GetFloat("Player Speed") > 0.1&&!animator.GetBool("Jumping"))
        {
            if (--soundDelay < 0)
            {
                soundDelay = FOOTSTEP_DELAY;
                var sounds = this.gameObject.GetComponent<PickRandomSound>();
                AudioSource.PlayClipAtPoint(sounds.GetRandom(), this.gameObject.transform.position, 1.0f);
            }
        }

        #endregion
    }

    //player attacks
    void Attack()
    {
        //attack animation
        animator.SetTrigger("Attack2");
        FindObjectOfType<AudioManager>().Play("player_attack");

        //detect enemies
        Collider2D[] enemiesHit = Physics2D.OverlapAreaAll(attackPoint1.position, attackPoint2.position, enemyLayers);

        //damage enemies
        foreach(Collider2D enemy in enemiesHit)
        {
            if(enemy.GetComponent<EnemyHurtbox>()!=null)
            {
                enemy.GetComponent<EnemyHurtbox>().TakeHit(attackDmg);
                FindObjectOfType<AudioManager>().Play("enemy_hurt");
            }
            if(enemy.GetComponent<VillagerHurtbox>()!=null)
            {
                enemy.GetComponent<VillagerHurtbox>().TakeHit(attackDmg);
                FindObjectOfType<AudioManager>().Play("enemy_hurt");
            }
            if (enemy.GetComponent<DummyHurtbox>() != null)
            {
                enemy.GetComponent<DummyHurtbox>().TakeHit(attackDmg);
                FindObjectOfType<AudioManager>().Play("enemy_hurt");
            }


            Debug.Log("we hit:" + enemy.name);
        }

    }

    //plays hurt animation
    public void GetHurt()
    {
        animator.SetTrigger("Hurt");
        FindObjectOfType<AudioManager>().Play("player_hurt");
    }

    //the enemy dies
    public void Die()
    {
        
        animator.SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        RevoceChanegables();
        PlayerPrefs.SetInt("game_over", 1);
        Debug.Log("Player died!");
        FindObjectOfType<AudioManager>().Play("game_over");
    }

    //when touching a coin - the coin gets destroyed
    //when touching a kill collider - the player dies
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            IncrementCoins();
            var sounds = other.gameObject.GetComponent<PickRandomSound>();
            AudioSource.PlayClipAtPoint(sounds.GetRandom(), other.gameObject.transform.position, 1.0f);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("KillPlayerInstantly"))
        {
            this.Die();
        }
        if(other.gameObject.CompareTag("EndLevelCollider"))
        {
            coinsThisLevel = coins;
            potionsThisLevel = potions;
            killedVillagersThisLevel = killedVillagers;
            PlayerPrefs.SetInt("coins", coinsThisLevel);
            PlayerPrefs.SetInt("potions", potionsThisLevel);
            PlayerPrefs.SetInt("villagers", killedVillagersThisLevel);
            Debug.Log("End level collider set the potions to: " + potionsThisLevel);
        }
    }

    private void RevoceChanegables()
    {
        coins = coinsThisLevel;
        potions = potionsThisLevel;
        killedVillagers = killedVillagersThisLevel;
        PlayerPrefs.SetInt("coins", coins);
        Debug.Log("coins set after death: " + coins);
        PlayerPrefs.SetInt("potions", potions);
        Debug.Log("potions set after death: " + potions);
        PlayerPrefs.SetInt("villagers", killedVillagers);
    }

    public void IncrementCoins()
    {
        coins++;
        PlayerPrefs.SetInt("coins", coins);
         if (textCoinCounter != null)
        {
            textCoinCounter.text = coins.ToString();
        }
        
    }

    //sets enchantment
    public void SetEnchanted(bool isEnchanted)
    {
        this.isEnchanted = isEnchanted;
        if(isEnchanted)
        {
            PlayerPrefs.SetInt("isEnchanted", 1);
        }
        else 
        {
            PlayerPrefs.SetInt("isEnchanted", 0);
        }
        
    }

    //tells us if the player is enchanted
    public bool IsPlayerEnchanted()
    {
        return this.isEnchanted;
    }

    //sets the amount of coins the player has
    public void SetCoins(int coins)
    {
        this.coins = coins;
        PlayerPrefs.SetInt("coins", coins);
        textCoinCounter.text = coins.ToString();
    }

    //returns the amount of coins the player has
    public int GetCoins()
    {
        return this.coins;
    }

    //sets the amount of potions the player has
    public void SetPotions(int potions)
    {
        Debug.Log("SetPotions set the potions to: " + potions);
        this.potions = potions;
        Debug.Log("PlayerSetPotions has set potions to: " + potions);
        textPotionsCounter.text = potions.ToString();
        Debug.Log("Potions set to: " + potions);
        PlayerPrefs.SetInt("potions", potions);
    }

    //returns the amount of potions the player has
    public int GetPotions()
    {
        return this.potions;
    }

    private void DrinkPotion()
    {
        if(potions>0)
        {
            this.GetComponentInChildren<PlayerHurtbox>().Heal(potionHealsBy);
            potions = potions - 1;
            Debug.Log("PlayerDrinkPotions has set potions to: " + potions);
            textPotionsCounter.text = potions.ToString();
            PlayerPrefs.SetInt("potions", potions);
        }
    }

    public void SetPotionsLevel(int potionsLevel)
    {
        potionsThisLevel = potionsLevel;
        Debug.Log("SetPotionsLevel set the potions per level to: " + potionsThisLevel);
        potions = potionsThisLevel;
        PlayerPrefs.SetInt("potions", potions);
        Debug.Log("SetPotionsLevel set the potions: " + potions);
    }

    //increases the amount of killed villagers by 1
    public void SetVillagerKilled()
    {
        killedVillagers++;
        PlayerPrefs.SetInt("villagers", killedVillagers);
        Debug.Log("Villagers killed so far: " + killedVillagers);
    }

    //returns the total amount of killed villagers
    public int GetVillagersKilled()
    {
        return this.killedVillagers;
    }

    //for the indexLevel
    public void SetIndexLevel(int index)
    {
        indexLevel = index;
        PlayerPrefs.SetInt("level", indexLevel);
    }

    //to return the index of the level
    public int GetIndexLevel()
    {
        indexLevel = PlayerPrefs.GetInt("level");
        return indexLevel;
    }
}


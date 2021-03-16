using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private EnemyControl controller;
    public enum Direction { LEFT, RIGHT, UP, DOWN, NONE }

    public Direction startGravityDirection = Direction.DOWN;

    //shows if the player entered the detection square - debug purpose
    public bool entered = false;

    float nextAtckTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<EnemyControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RotateAndAttack(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        RotateAndAttack(other);
    }

    private void RotateAndAttack(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerControl player = FindObjectOfType<PlayerControl>();
            if (player != null)
            {
                
                Vector2 relative = transform.position - other.transform.position;
                Direction collisionDirection;

                if (relative.x > 0.0f)
                    collisionDirection = Direction.LEFT;
                else
                    collisionDirection = Direction.RIGHT;

                if (controller.direction == 1)
                {
                    if (collisionDirection == Direction.LEFT)
                    {
                        controller.GetComponent<SpriteRenderer>().flipX = !controller.GetComponent<SpriteRenderer>().flipX;
                        controller.direction = -1;
                    }
                }
                else
                {
                    if (collisionDirection == Direction.RIGHT)
                    {
                        controller.GetComponent<SpriteRenderer>().flipX = !controller.GetComponent<SpriteRenderer>().flipX;
                        controller.direction = 1;
                    }
                }
                
                if (Time.time >= nextAtckTime)
                {
                    controller.Attack();
                    nextAtckTime = Time.time + 1f / controller.attackRate;
                }
                    
                entered = true;
            }
        }
        else
        {
            controller.StopAttack();
        }
    }
}

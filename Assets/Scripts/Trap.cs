using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int dmgTaken = 10;
    public Transform point1;
    public float radius = 2;
    float nextAtckTime = 0f;

    private void OnCollisionStay2D(Collision2D other)
    {
        HurtThem(other);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        HurtThem(other);
    }

    private void HurtThem(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && Time.time >= nextAtckTime)
        {
            PlayerControl player = other.gameObject.GetComponent<PlayerControl>();

            if (player != null)
            {
                player.GetComponentInChildren<PlayerHurtbox>().TakeHit(dmgTaken);
                nextAtckTime = Time.time + 1f / 2f;
            }
        }
        if(other.gameObject.CompareTag("Enemy") && Time.time >= nextAtckTime)
        {
            EnemyControl enemy = other.gameObject.GetComponent<EnemyControl>();
            if(enemy!=null)
            {
                enemy.GetComponentInChildren<EnemyHurtbox>().TakeHit(dmgTaken);
                nextAtckTime = Time.time + 1f / 2f;
            }
        }
        
    }
    

}

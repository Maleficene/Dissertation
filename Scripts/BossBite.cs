using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBite : MonoBehaviour
{
    public int attackDamage = 40;
    public float enemyKnockbackAmount;
    public Animator animator;
    public Rigidbody2D rb;


    //When the Boss bites the player animator values are set to initiate the next animation
    //The player takes damage from the object on collision and the object gets knocked back using the its rigidbody
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(attackDamage);
                animator.SetBool("Attacking", true);
                Invoke("BossAttackReset", 0.8f);
                Vector2 enemyKnockback = rb.transform.position - other.transform.position;
                enemyKnockback.y = 0;
                rb.AddForce(enemyKnockback * enemyKnockbackAmount);

            }
        }
    }

    //Resets the boss attack boolean
    void BossAttackReset()
    {
        animator.SetBool("Attacking", false);

    }

   



}

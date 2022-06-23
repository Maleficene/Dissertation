using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainMoleAnim : MonoBehaviour
{
    public Animator animator;
    //calls the attack animaion 
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                animator.SetBool("Attacking", true);
                Invoke("EnemyAttackReset", 0.1f);

            }
        }
    }
    void EnemyAttackReset()
    {
        animator.SetBool("Attacking", false);
    }

}

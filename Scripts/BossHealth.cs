using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public static BossHealth instance;
    public static float health = 800;
    public static float healthBarScale = 1.6f;
    public static bool bossDead = false;
    public Animator animator;


    private void Awake()
    {
        healthBarScale = Mathf.Clamp(healthBarScale, 0, 1.6f);
    }


    //Allows the calling of the method within the bullet script to deal damage to the Boss
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBarScale -= 0.1f;

        if (health <= 0)
        {
            bossDead = true;
            DeathAnim();
        }
    }

    //triggers the death animtion
    void DeathAnim()
    {
        animator.SetTrigger("Dead");
      
    }


  




}

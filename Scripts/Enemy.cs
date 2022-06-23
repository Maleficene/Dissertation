using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;


    public void Update()
    {
        //Debug.Log(health);
    }

    //Allows the calling of the method within the bullet script to deal damage to the enemy
    public void TakeDamage (int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    //destroys the game object
    void Die()
    {
        Destroy(gameObject);
    }
}

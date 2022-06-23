using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Damage variables
    public float speed = 20f;
    public Rigidbody2D rb;
    public int bulletDamage = 50;
    public float bulletKnockbackAmount;

    // Start is called before the first frame update
    void Start()
    {
        //allows the bullet to move
        rb.velocity = transform.TransformDirection(Vector3.left) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Destroys the bullet when enemy is collided with, and also makes the enemy take damage
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        BossHealth boss = hitInfo.GetComponent<BossHealth>();
        if (hitInfo.CompareTag("Enemy") || hitInfo.CompareTag("Wall") || hitInfo.CompareTag("Ground")) { 
        if(enemy != null)
        {
            enemy.TakeDamage(bulletDamage);
            Vector2 bulletKnockback = hitInfo.transform.position - rb.transform.position;
            bulletKnockback.y = 0;
            hitInfo.gameObject.GetComponent<Rigidbody2D>().AddForce(bulletKnockback.normalized * bulletKnockbackAmount);

        }   //Destroys the bullet when the boss is collided with, and also makes them take damage
         else if (boss != null)
            {
                boss.TakeDamage(bulletDamage);
                
                Vector2 bulletKnockback = hitInfo.transform.position - rb.transform.position;
                bulletKnockback.y = 0;
                hitInfo.gameObject.GetComponent<Rigidbody2D>().AddForce(bulletKnockback.normalized * bulletKnockbackAmount);
            }
        Destroy(gameObject);
    }
}
}

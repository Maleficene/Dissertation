using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private bool isTriggered = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || isTriggered == false){
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                //kills the player when it collides with the spike
                player.TakeDamage(HealthBar.instance.currentHealth);
            }
        }else if (other.gameObject.CompareTag("Enemy"))
        {
            //kills the player when it collides with the spike
            Destroy(other.gameObject);
        }
        isTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip collectSound;
    public int value = 1;

    //adds score to the players user interface
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            ScoreManager.instance.ChangeScore(value);
            Destroy(gameObject);
        }
    }
}

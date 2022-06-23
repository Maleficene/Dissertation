using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int health;
    public float[] position;
    public int currentLives;
    public int score;
    public int level;

    //data saved for the player to be able to reload back into the game where necessary
    public PlayerData(PlayerController playerInfo)
    {
        health = HealthBar.instance.currentHealth;
        currentLives = LifeManager.instance.currentLives;
        score = ScoreManager.instance.score;
        level = LevelStatus.instance.level;

        position = new float[3];
        position[0] = playerInfo.transform.position.x;
        position[1] = playerInfo.transform.position.y;
        position[2] = playerInfo.transform.position.z;
    }
}

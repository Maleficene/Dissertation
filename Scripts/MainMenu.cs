using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // starts the game by pushing the active scene to the next
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }

    //loads the last saved instance of the game
    public void LoadPlayer()
    {
        PlayerData data = Saving.LoadSave();
        HealthBar.instance.currentHealth = data.health;
        LifeManager.instance.currentLives = data.currentLives;
        ScoreManager.instance.score = data.score;
        SceneManager.LoadScene(data.level);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

    }



    //quits the game
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}

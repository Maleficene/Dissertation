using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseUI;
    public GameObject FailUI;
    
   

    // Update is called once per frame
    void Update()
    {
        //pauses and unpauses the game based on game state when escape key is presed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        
    }

    private void FixedUpdate()
    {
        if (LifeManager.instance.currentLives <= 0)
        {
           LevelFailed();
            
       }
    }

    //Sets timescale to normal removes the pauseUI
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    //Sets timescale to freeze the game and activates the pauseUI
    void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    //calls the level failed screen
    void LevelFailed()
    {
        FailUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    //restarts the level 
    public void RestartLevel()
    {
        FailUI.SetActive(false);
        Reload();
        Time.timeScale = 1f;
        GamePaused = false;
        PlayerController.controlsDisabled = false;
        LifeManager.instance.ChangeLives(3);
        HealthBar.instance.MaxHealth(HealthBar.instance.maxHealth);
        HealthBar.instance.currentHealth = HealthBar.instance.maxHealth;
        
    }

    //reloads the current scene
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

   //Load the players data from a save
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

        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }


    //Loads to main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    //Quits game
    public void quitGame()
    {
        Application.Quit();
    }


}

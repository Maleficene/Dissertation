using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GameObject winUI;

    // Update is called once per frame
    void Update()
    {

        //when the boss is dead set the win screen to true
        if (BossHealth.bossDead == true)
        {
            Invoke("WinSuccess", 2f);

        }
        Debug.Log(BossHealth.health);
        Debug.Log(BossHealth.bossDead);
    }

    void WinSuccess()
    {
        winUI.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        winUI.SetActive(false);
        Time.timeScale = 1f;
        BossHealth.bossDead = false;
    }


    //Quits game
    public void quitGame()
    {
        Application.Quit();
    }

}

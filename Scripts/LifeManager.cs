using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;
    public TextMeshProUGUI text;
    public int maxLives = 3;
    public int currentLives;
    


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Start()
    {
        currentLives = maxLives;
        text.text = "x " + currentLives.ToString();
    }

  
    //allows the player to lose or gain lives
    public void ChangeLives (int value)
    {
        currentLives += value;
        text.text = "x " + currentLives.ToString();
    }
}

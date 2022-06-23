using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStatus : MonoBehaviour
{
    public static LevelStatus instance;
    public int level;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(instance);
        }
    }

    //used to record the current level for saving
    private void Update()
    {
        level = SceneManager.GetActiveScene().buildIndex;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnhideCross : MonoBehaviour
{

    public static UnhideCross instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            
        }

       
    }
    
    //methods to set the gravity icons (not able to change gravity) to true or false

    public void ShowCrossIcon()
    {
        gameObject.SetActive(true);
    }

    public void HideCrossIcon()
    {
        gameObject.SetActive(false);
    }
}

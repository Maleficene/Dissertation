using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnhideStar : MonoBehaviour
{

    public static UnhideStar instance;
    
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

    //methods to set the gravity icons(not able to change gravity) to true or false

    public void ShowStarIcon()
    {
        gameObject.SetActive(true);
    }

    public void HideStarIcon()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{

    public DisablePopUp dialogCheck;
    public string popUp;
    public int readAmount = 1;


    private void Awake()
    {
       dialogCheck = GameObject.FindGameObjectWithTag("Dialog").GetComponent<DisablePopUp>();
        
    }

    //when the player walks across the sign a dialog box appears presenting the player with custom set text
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && readAmount < 3 && dialogCheck.popUpActive == false)
        {
            
            PopUpText pop = GameObject.FindGameObjectWithTag("Data").GetComponent<PopUpText>();
            pop.PopUp(popUp);
            readAmount++;
            dialogCheck.popUpActive = true;
        }
    }

    

}

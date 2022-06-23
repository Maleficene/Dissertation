using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePopUp : MonoBehaviour
{
    public bool popUpActive = false;
    Animator popUp;
    // Start is called before the first frame update
    void Start()
    {
        popUp = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && popUpActive == true)
        {
            popUp.SetTrigger("close");
            popUpActive = false;
           
        }
    }
}

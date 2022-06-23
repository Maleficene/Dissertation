using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    //keeps record of checkpoint locations
    public Vector2 lastCheckpointPosition;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
        }else
        {
            Destroy(gameObject);
        }
    }

   
}

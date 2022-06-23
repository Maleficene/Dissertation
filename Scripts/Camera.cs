using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private GameObject player;
    [Header("Camera Limits")]
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // Start is called before the first frame update
    void Start()
    {
        //Find the player game object
        player = GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        //clamp the camera on the x and y values that are assigned in the inspector
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        //moves the game objects on the z and clamped x and y axis
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}

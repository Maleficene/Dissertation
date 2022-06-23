using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //scales the bosses health with damage dealt
        localScale.x = BossHealth.healthBarScale;
        transform.localScale = localScale;
    }
}

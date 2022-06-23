using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletRechargeBar : MonoBehaviour
{
    public Slider rechargeBar;
    private int maxRecharge = 100;
    public int currentRecharge;
    private WaitForSeconds rechargeTime = new WaitForSeconds(0.1f);

    public static BulletRechargeBar instance;

    private void Awake()
    {
        if(instance != null)
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
        currentRecharge = maxRecharge;
        rechargeBar.maxValue = maxRecharge;
        rechargeBar.value = maxRecharge;
    }

    public void UseRecharge(int drain)
    {
        //takes an amount from the user interface bullet bar and initiates the sliders recharge
        if(currentRecharge - drain >= 0)
        {
            currentRecharge -= drain;
            rechargeBar.value = currentRecharge;

            StartCoroutine(RegenBullets());
        }
    }

    private IEnumerator RegenBullets()
    {
        //wait 2 seconds before a recharge
        yield return new WaitForSeconds(2);

        //adds to the bar progressively until the maximum charge is reached
        while(currentRecharge < maxRecharge)
        {
            currentRecharge += maxRecharge / 100;
            rechargeBar.value = currentRecharge;
            yield return rechargeTime;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public int maxHealth = 100;
    public int currentHealth;

    public static HealthBar instance;

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
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }


    //sets the health of the player
    public void MaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    

    //allows the player to lose health
    public void HealthAmount(int health)
    {
        currentHealth -= health;
        slider.value = currentHealth;

        fill.color = gradient.Evaluate(slider.normalizedValue); 
    }
}

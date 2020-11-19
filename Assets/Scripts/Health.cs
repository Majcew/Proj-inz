﻿using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text healthText;
    public Text healthTextNumber;
    private Player_Movement playerspeeds;
    private float originalWalkingSpeed;
    private float originalRunningSpeed;
    public float health;
    public float maxHealth = 100f;
    public Slider healthSlider;

    bool isDead;

    private void Awake()
    {
        playerspeeds = GetComponent<Player_Movement>();
        originalRunningSpeed = GetComponent<Player_Movement>().playerRunningSpeed;
        originalWalkingSpeed = GetComponent<Player_Movement>().playerWalkingSpeed;
    }

    public void CheckHP()
    {
        if (health <= 30)
        {
            playerspeeds.playerWalkingSpeed = originalWalkingSpeed * 0.3f;
            playerspeeds.playerRunningSpeed = originalRunningSpeed * 0.3f;
        }
        if (health <= 60 && health > 30)
        {
            playerspeeds.playerWalkingSpeed = originalWalkingSpeed * 0.75f;
            playerspeeds.playerRunningSpeed = originalRunningSpeed * 0.75f;
        }
        if (health > 60)
        {
            playerspeeds.playerWalkingSpeed = originalWalkingSpeed;
            playerspeeds.playerRunningSpeed = originalRunningSpeed;
        }

    }

    void Start()
    {
        // health = 100f;
        health = maxHealth;

        SetHealthText();
    }

    public void SetHealthText()
    {
        //healthText.text = "Lives: " + health.ToString();
        healthTextNumber.text = health.ToString();
        healthSlider.value = health;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            health = 0;
        }
        SetHealthText();
        CheckHP();
    }
}
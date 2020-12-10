using Mirror;
using System;
using UnityEngine.UI;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public Text healthText;
    public Text healthTextNumber;
    private Player_Movement playerspeeds;
    private float originalWalkingSpeed;
    private float originalRunningSpeed;
    [SyncVar]
    public float health;
    public float maxHealth = 100f;
    public Slider healthSlider;
    [SerializeField]
    private RespawnPlayer respawnPlayer;

    [SyncVar]
    bool isDead;

    private void Start()
    {
        if (isLocalPlayer)
        {
            playerspeeds = GetComponent<Player_Movement>();
            originalRunningSpeed = GetComponent<Player_Movement>().playerRunningSpeed;
            originalWalkingSpeed = GetComponent<Player_Movement>().playerWalkingSpeed;
            respawnPlayer.Setup();
        }
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


    void SetHealthText()
    {
        //healthText.text = "Lives: " + health.ToString();
        healthTextNumber.text = health.ToString();
        healthSlider.value = health;
    }
    public bool RestoreHP(int amount)
    {
        if (health == maxHealth)
        {
            return true;
        }
        else if (health + amount > maxHealth)
        {
            health = 100f;
            SetHealthText();
            CheckHP();
            return false;
        }
        else
        {
            health += amount;
            SetHealthText();
            CheckHP();
            return false;
        }
    }

    [ClientRpc]
    public void RcpTakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            health = 0;
        }
        SetHealthText();
    }

    public void ResetHealth()
    {
        isDead = false;
        health = maxHealth;
        SetHealthText();
    }

    private void Update()
    {
        if (isDead)
        {
            respawnPlayer.disableBehaviourComponents();
            Transform respPosRotData = NetworkManager.singleton.GetStartPosition();
            this.transform.position = respPosRotData.position;
            this.transform.rotation = respPosRotData.rotation;
            respawnPlayer.enableBehaviourComponents();
        }
    }
}
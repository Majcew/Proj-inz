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
    [SyncVar]
    public int deadCount;
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
            deadCount = 0;
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
    public bool RestoreHPPossible(int amount)
    {
        if (amount == 100)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }

    [ClientRpc]
    public void RcpRestoreHP(int amount)
    {
    
        if (health + amount > maxHealth)
        {
            health = 100f;
            SetHealthText();
            //CheckHP();
        }
        else
        {
            health += amount;
            SetHealthText();
            //CheckHP();
        }
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
    }

    [Command]
    public void CmdResetHealth()
    {
        isDead = false;
        deadCount += 1;
        health = maxHealth;
        RcpSetHealthText();
    }
    [ClientRpc]
    public void RcpSetHealthText()
    {
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
using Mirror;
using UnityEngine.UI;

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

    [SyncVar]
    bool isDead;

    private void Start()
    {
        if (isLocalPlayer)
        {
            playerspeeds = GetComponent<Player_Movement>();
            originalRunningSpeed = GetComponent<Player_Movement>().playerRunningSpeed;
            originalWalkingSpeed = GetComponent<Player_Movement>().playerWalkingSpeed;
            health = maxHealth;
            CmdSetHealthText();
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

    [Command]
    public void CmdSetHealthText()
    {
        RpcSetHealthText();
    }

    [ClientRpc]
    void RpcSetHealthText()
    {
        //healthText.text = "Lives: " + health.ToString();
        healthTextNumber.text = health.ToString();
        healthSlider.value = health;
    }
    [ClientRpc]
    public void RpcTakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            health = 0;
        }
        CmdSetHealthText();
    }

    private void Update()
    {
        if (isDead)
        {
            Destroy(gameObject);
        }
    }
}
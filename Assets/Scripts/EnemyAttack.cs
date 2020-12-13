using UnityEngine;
using Mirror;

public class EnemyAttack : NetworkBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public float attackDamage = 10f;

    Animator anim;
    GameObject player;

    Health health;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    int targetDeadCount;

    void Awake()
    {
        playerInRange = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { 
            player = other.gameObject;
            Debug.Log(player);
            health = GameManager.GetPlayerHealth(player.GetComponent<NetworkIdentity>().netId.ToString());
            Debug.Log(health);
            enemyHealth = GameManager.GetEnemyHealth(this.netId.ToString());
            //anim = GetComponent<Animator>();
            //sprawdzanie czy gracz jest dość blisko by go zaatakować
            if (other.gameObject == player)
            {
                targetDeadCount = health.deadCount;
                playerInRange = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        //if (player == null) player.GetComponent<Health>();
        //sprawdzenie czy możemy zaatakować przeciwnika (timer)
        if (playerInRange)
        {
            if (health != null)
            {
                timer += Time.deltaTime;
                if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
                { 
                    Attack();
                }

            }
        }
       
    }
    void Attack()
    {
        timer = 0f;
        //event do zmiany animacji
        //anim.SetTrigger("AttackPlayer");
        if (health.health > 0 && playerInRange && targetDeadCount == health.deadCount)
        {
            health.RpcTakeDamage(attackDamage);
        }
        else
        {
            playerInRange = false;
            //event do zmianyyy animacji
            //anim.SetTrigger("PlayerDead");
        }
    }
}

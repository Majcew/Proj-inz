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
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<Health>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        //sprawdzanie czy gracz jest dość blisko by go zaatakować
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //sprawdzenie czy gracz nie uciekł
        if (other.gameObject == player)
        {
            playerInRange = false;
            //event do zmiany animacji
            anim.SetTrigger("PlayerOutOfRange");
        }
    }

    void Update()
    {
        //sprawdzenie czy możemy zaatakować przeciwnika (timer)
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            CmdAttack();
        }
        if (health.health <= 0)
        {
            //event do zmianyyy animacji
            anim.SetTrigger("PlayerDead");
        }
    }
    [Command]
    void CmdAttack()
    {
        timer = 0f;
        //event do zmiany animacji
        anim.SetTrigger("AttackPlayer");
        if (health.health > 0)
        {
            health.RcpTakeDamage(attackDamage);
        }
    }
}

using UnityEngine;
using Mirror;

public class EnemyAttack : NetworkBehaviour
{
    private float nextAttack = 0f;
    public float timeBetweenAttacks = 0.5f;
    public float attackDamage = 10f;

    Animator anim;
    Transform player;

    Health health;
    EnemyHealth enemyHealth;
    float timer;
    int targetDeadCount;

    [SerializeField]
    private float attackRange;
    [SerializeField]
    private LayerMask whatIsPlayer;
    private UnityEngine.AI.NavMeshAgent nav;

    private bool playerInAttackRange;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Player")) { 
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
        }*/
    }

    void OnTriggerExit(Collider other)
    {
        /*player = GameObject.FindGameObjectWithTag("Player");
        {
            playerInRange = false;
        }*/
    }

    void Update()
    {
        //if (player == null) player.GetComponent<Health>();
        //sprawdzenie czy możemy zaatakować przeciwnika (timer)
        /*if (playerInRange)
        {
            if (health != null)
            {
                timer += Time.deltaTime;
                if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
                { 
                    Attack();
                }

            }
        }*/
        nextAttack += Time.deltaTime;
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInAttackRange)
        {
            player = Physics.OverlapSphere(transform.position, attackRange, whatIsPlayer)[0].transform;
            health = player.GetComponent<Health>();
            nav.SetDestination(transform.position);
            transform.LookAt(player);
            if(nextAttack > timeBetweenAttacks)
            {
                Attack();
                nextAttack = 0f;
            }
        }
        else
        {
            anim.SetBool("AttackPlayer", false);
        }
    }
    public void Attack()
    {
        timer = 0f;
        //event do zmiany animacji
        anim.SetBool("AttackPlayer", true);
        if (health.health > 0)
        {
            health.RpcTakeDamage(attackDamage);
        }
        else
        {
            //event do zmianyyy animacji
            //anim.SetTrigger("PlayerDead");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

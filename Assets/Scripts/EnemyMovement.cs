using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : NetworkBehaviour
{
    [SerializeField]
    private float speed;
    Transform playerTransform;
    Transform player;
    Health health;
    NetworkIdentity networkIdentity;
    EnemyHealth enemyHealth;
    public UnityEngine.AI.NavMeshAgent nav;

    public LayerMask whatIsGround, whatIsPlayer;
    private bool playerInSightRange;
    [SerializeField]
    private float sightRange,attackRange;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    void Awake()
    {
        //szukanie gracza
        /*
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = player.GetComponent<Health>();
        enemyHealth = GetComponent<EnemyHealth>();*/
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //szukanie gracza
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (playerInSightRange) { 
            player = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer)[0].transform;
            ChasePlayer();
        }
        if(!playerInSightRange) {
            player = null;
            Patroling();
        };
        /*if (playerInSightRange) {
            //players = GameObject.FindGameObjectsWithTag("Player");
            player = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer)[0].transform;
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            *//*player = players[0].transform;
            playerTransform = player.transform;*//*
            networkIdentity = player.GetComponent<NetworkIdentity>();
             if (networkIdentity != null)
             {
                health = GameManager.GetPlayerHealth(networkIdentity.netId.ToString());
             }
             float current_obj_dist = Vector3.Distance(player.transform.position, this.transform.position);
             if (current_obj_dist < distance)
             {
                distance = current_obj_dist;
                *//*player = players[i].transform;
                playerTransform = player.transform;*//*
                player = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer)[0].transform;
                networkIdentity = player.GetComponent<NetworkIdentity>();
                if (networkIdentity != null)
                {
                    health = GameManager.GetPlayerHealth(networkIdentity.netId.ToString());
                }
             }
        }*/

        enemyHealth = GameManager.GetEnemyHealth(this.netId.ToString());

        /*if (playerTransform != null && health != null && enemyHealth != null && nav != null)
        {
            if (enemyHealth.currentHealth > 0 && health.health > 0)
            {
                nav.enabled = true;
                nav.speed = speed;
                nav.SetDestination(playerTransform.position);
            }
            else
            {
                nav.enabled = false;
            }
        }
        else
        {
            Debug.Log("Sth is null in Enemy Movemet");
        }*/
        
    }
    private void ChasePlayer()
    {
        nav.SetDestination(player.position);
    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            nav.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

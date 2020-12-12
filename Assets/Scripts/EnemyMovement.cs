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
    GameObject[] players;
    GameObject player;
    Health health;
    NetworkIdentity networkIdentity;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
/*    void Awake()
    {
        //szukanie gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = player.GetComponent<Health>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }*/

    // Update is called once per frame
    void Update()
    {
        //szukanie gracza

        players = GameObject.FindGameObjectsWithTag("Player");
        if (players != null)
        {
            if(players.Length > 0)
            {
                float distance = Vector3.Distance(players[0].transform.position, this.transform.position);
                player = players[0];
                playerTransform = player.transform;
                networkIdentity = player.GetComponent<NetworkIdentity>();
                if (networkIdentity != null)
                {
                    health = GameManager.GetPlayerHealth(networkIdentity.netId.ToString());
                }

                for (int i = 0; i < players.Length; i++)
                {
                    float current_obj_dist = Vector3.Distance(players[i].transform.position, this.transform.position);
                    if (current_obj_dist < distance)
                    {
                        distance = current_obj_dist;
                        player = players[i];
                        playerTransform = player.transform;
                        networkIdentity = player.GetComponent<NetworkIdentity>();
                        if (networkIdentity != null)
                        {
                            health = GameManager.GetPlayerHealth(networkIdentity.netId.ToString());
                        }
                    }
                }
            }
        }

        enemyHealth = GameManager.GetEnemyHealth(this.netId.ToString());
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (playerTransform != null && health != null && enemyHealth != null && nav != null)
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
        }
        
    }
}

using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Dictionary<string, Health> playerHealths = new Dictionary<string, Health>();
    private static Dictionary<string, PlayerViewManager> playerViews = new Dictionary<string, PlayerViewManager>();
    private static Dictionary<string, EnemyHealth> enemyHealths = new Dictionary<string, EnemyHealth>();
    private static int itemsCount = 0;
    private static bool keyState = false;

    public static void AddPlayerHealth(string netId, Health player_health)
    {
        string name = "Player" + netId;
        player_health.transform.name = name;
        playerHealths.Add(netId, player_health);
        Debug.Log("PlayerHealth of " + name + " added");
    }

    public static void AddPlayerView(string netId, PlayerViewManager player_view)
    {
        string name = "Player" + netId;
        playerViews.Add(netId, player_view);
        Debug.Log("PlayerViews of " + name + " added");
    }

    public static void AddEnemyHealth(string netId, EnemyHealth enemy_health)
    {
        string name = "Enemy" + netId;
        enemy_health.transform.name = name;
        if (!enemyHealths.ContainsKey(netId)) enemyHealths.Add(netId, enemy_health);
        else 
        {
            
            enemyHealths.Remove(netId);
            enemyHealths.Add(netId, enemy_health);
        }
        Debug.Log("EnemyHealth of " + name + " added");
    }

    public static void RemovePlayerHealth(string netId)
    {
        playerHealths.Remove(netId);
        Debug.Log("PlayerHealth of Player" + netId + " removed");
    }

    public static void RemoveAllPlayers()
    {
        for(int i=0; i<playerHealths.Values.Count; i++)
        {
            GameObject player = playerHealths.Values.ElementAt<Health>(i).gameObject;
            Debug.Log("ds: " + player.name);

            Destroy(playerHealths.Values.ElementAt<Health>(i).gameObject);
        }
    }

    public static void RemovePlayerView(string netId)
    {
        playerViews.Remove(netId);
        Debug.Log("PlayerViews of Player" + netId + " removed");
    }

    public static Health GetPlayerHealth(string netId)
    {
        return playerHealths[netId];
    }

    public static EnemyHealth GetEnemyHealth(string netId)
    {
        return enemyHealths[netId];
    }

    public static void AddItemCount()
    {
        itemsCount++;
    }

    public static Dictionary<string, PlayerViewManager> GetPlayerViews()
    {
        return playerViews;
    }
    public static void AddKey()
    {
        keyState = true;
    }
    public static int GetItemCount()
    {
        return itemsCount;
    }
    public static bool GetKeyState()
    {
        return keyState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Dictionary<string, Health> playerHealths = new Dictionary<string, Health>();

    public static void AddPlayerHealth (string netId, Health player_health)
    {
        string name = "Player" + netId;
        player_health.transform.name = name;
        playerHealths.Add(netId, player_health);
        Debug.Log("PlayerHealth of " + name + " added");
    }

    public static void RemovePlayerHealth(string netId)
    {
        playerHealths.Remove(netId);
        Debug.Log("PlayerHealth of Player" + netId + " removed");
    }

    public static Health GetPlayerHealth(string netId)
    {
        return playerHealths[netId];
    }
}

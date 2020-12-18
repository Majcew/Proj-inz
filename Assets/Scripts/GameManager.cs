﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Dictionary<string, Health> playerHealths = new Dictionary<string, Health>();
    private static Dictionary<string, EnemyHealth> enemyHealths = new Dictionary<string, EnemyHealth>();
    private static int itemsCount = 0;
    private static bool keyTaken = false;

    public static void AddPlayerHealth (string netId, Health player_health)
    {
        string name = "Player" + netId;
        player_health.transform.name = name;
        playerHealths.Add(netId, player_health);
        Debug.Log("PlayerHealth of " + name + " added");
    }

    public static void AddEnemyHealth(string netId, EnemyHealth enemy_health)
    {
        string name = "Enemy" + netId;
        enemy_health.transform.name = name;
        enemyHealths.Add(netId, enemy_health);
        Debug.Log("EnemyHealth of " + name + " added");
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

    public static EnemyHealth GetEnemyHealth(string netId)
    {
        return enemyHealths[netId];
    }

    public static void AddItemCount()
    {
        itemsCount++;
    }

    public static void UpdatePlayerItemCount()
    {
        //TODO: Funkcja uzupełniająca w UI gracza liczbę itemsów zdobytych.
    }
    public static void UpdateKeyPossesion(bool state)
    {
        //TODO: Funkcja dodająca ikonkę klucza do UI gracza.
    }
}

using UnityEngine;
using Mirror;

public class ServerSpawner : NetworkBehaviour
{
    public static void SpawnObject(GameObject obj)
    {
        CmdSpawnObject(obj);
    }
    private static void CmdSpawnObject(GameObject obj)
    {
        Debug.Log("spawn");
        NetworkServer.Spawn(obj);
    }
}

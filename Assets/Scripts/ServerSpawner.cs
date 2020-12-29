using UnityEngine;
using Mirror;

public class ServerSpawner : NetworkBehaviour
{
    [SerializeField]
    private float timeBetweenSpawns;
    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private float y_offset;
    private Vector3 position;
    private float timer;

    private void Start()
    {
        position = gameObject.transform.position;
    }
    private void Update()
    {
       timer += Time.deltaTime;
       if(timer > timeBetweenSpawns)
        {
            CmdSpawnPrefab();
            timer = 0f;
        }
    }
    [Command(ignoreAuthority = true)]
    private void CmdSpawnPrefab()
    {
        int index = Random.Range(0, prefabs.Length);
        RpcSpawnPrefab(index);
    }
    [ClientRpc]
    private void RpcSpawnPrefab(int index)
    {
        GameObject toRender = Instantiate(prefabs[index], position + new Vector3(0,y_offset,0), Quaternion.identity);
        NetworkServer.Spawn(toRender);
    }
}

using UnityEngine;
using Mirror;

public class BossDoorBehaviour : NetworkBehaviour
{
    [SerializeField]
    private int itemAmountReq;
    [SerializeField]
    private GameObject[] msg;
    [SerializeField]
    private GameObject door;
    private int objectInsideCollider = 0;
    private void OnTriggerEnter(Collider other)
    {
        objectInsideCollider++;
        if(other.CompareTag("Player"))
        CmdCheckRequirements();
    }

    private void OnTriggerExit(Collider other)
    {
        objectInsideCollider--;
        if(objectInsideCollider == 0)
        {
            CmdShowMessages(false);
        }
    }
    [Command(ignoreAuthority = true)]
    void CmdCheckRequirements()
    {
        if(GameManager.GetItemCount() == itemAmountReq)
        {
            //Można tutaj dodać animację przesuwania drzwi. W tej chwili po prostu niszczę obiekt.
            RpcOpenDoor();
        } else
        {
            RpcShowMessages(true);
        }
    }
    [ClientRpc]
    void RpcOpenDoor()
    {
        Destroy(door);
        Destroy(this.gameObject);
    }
    [Command]
    void CmdShowMessages(bool state)
    {
        RpcShowMessages(state);
    }
    [ClientRpc]
    void RpcShowMessages(bool state)
    {
        foreach (GameObject c in msg)
        {
            c.SetActive(state);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Collections.Generic;
using Mirror.RemoteCalls;
using System.Collections;
using System.Linq;

public class PickupObject : NetworkBehaviour
{
    [SerializeField]
    private GameObject pickupText;
    [SerializeField]
    private Text itemsCountText;
    [SerializeField]
    private GameObject keyImage;
    private int itemCount;
    private bool keyState;
    private GameObject collidingItem;


    private void Awake()
    {
        itemsCountText.text = itemCount+"/6";
    }
    void Update() {
        if (Input.GetKeyDown("f") && collidingItem != null && !collidingItem.CompareTag("zombie"))
        {
            switch (collidingItem.tag)
            {
                case "health":
                    //CmdAddHealth();
                    CmdAddHealth(collidingItem.GetComponent<HealthBox>().restoreHealth);
                    break;
                case "ammunition":
                    //CmdAddAmmo();
                    CmdAddAmmo(collidingItem.GetComponent<PickupableAmmo>().index, collidingItem.GetComponent<PickupableAmmo>().amount);
                    break;
                case "item":
                    CmdUpdateItemsTaken();
                    Destroy(collidingItem);
                    collidingItem = null;
                    break;
                case "key":
                    CmdUpdateKeyState();
                    Destroy(collidingItem);
                    collidingItem = null;
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("health") || other.CompareTag("ammunition") || other.CompareTag("item") || other.CompareTag("key")) {
            collidingItem = other.gameObject;
            pickupText.SetActive(true); 
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        collidingItem = null;
        pickupText.SetActive(false);
    }
    [Command]
    void CmdAddHealth(int amount)
    {
        Health player_health = GameManager.GetPlayerHealth(this.netId.ToString());
        if (player_health.RestoreHPPossible(amount))
        {
            player_health.RcpRestoreHP(amount);
            RpcDestroyGameObject(collidingItem);
        }
    }

    [Command]
    void CmdAddAmmo(int index,int amount)
    {
        RpcAddAmmo(index, amount);
    }

    [ClientRpc]
    void RpcAddAmmo(int index, int amount)
    {
        Ammunition player_ammunition = GetComponent<Ammunition>();
        Destroy(collidingItem);
        player_ammunition.AddAmmunition(index, amount);
        pickupText.SetActive(false);
    }
    [Command]
    public void CmdUpdateItemsTaken()
    {
        GameManager.AddItemCount();
        RpcSetItemsCountText();
    }
    [Command]
    public void CmdUpdateKeyState()
    {
        GameManager.AddKey();
        RpcSetKeyImage();
    }
    /// <summary>
    /// Destroys the referenced GameObject on all client instances.
    /// </summary>
    [ClientRpc]
    private void RpcDestroyGameObject(GameObject referenced)
    {
        Destroy(referenced);
        pickupText.SetActive(false);
    }

    [ClientRpc]
    public void RpcSetItemsCountText()
    {
        Dictionary<string, PlayerViewManager>  playerViews = GameManager.GetPlayerViews();
        for (int i = 0; i < playerViews.Count; i++)
        {
           playerViews.Values.ElementAt(i).SetItemCountText();
        }
        Destroy(collidingItem);
        pickupText.SetActive(false);
    }
    [ClientRpc]
    public void RpcSetKeyImage()
    {
        Dictionary<string, PlayerViewManager> playerViews = GameManager.GetPlayerViews();
        for (int i = 0; i < playerViews.Count; i++)
        {
            playerViews.Values.ElementAt(i).SetKeyStateImage();
        }
        Destroy(collidingItem);
        pickupText.SetActive(false);
    }
}
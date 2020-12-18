using UnityEngine;
using UnityEngine.UI;
using Mirror;

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
            }
        }
        UpdateItemsTaken();
        UpdateKeyTaken();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("health") || other.CompareTag("ammunition")) {
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
            Destroy(collidingItem);
            pickupText.SetActive(false);
        }
    }

    [Command]
    void CmdAddAmmo(int index,int amount)
    {
        RpcAddAmmo(index, amount);
        Destroy(collidingItem);
    }

    [ClientRpc]
    void RpcAddAmmo(int index, int amount)
    {
        Ammunition player_ammunition = GetComponent<Ammunition>();
        player_ammunition.AddAmmunition(index, amount);
        pickupText.SetActive(false);
    }
    public void UpdateItemsTaken()
    {
        itemCount = GameManager.GetItemCount();
        itemsCountText.text = itemCount + "/6";
    }
    public void UpdateKeyTaken()
    {
        keyState = GameManager.GetKeyState();
        keyImage.SetActive(keyState);
    }
}
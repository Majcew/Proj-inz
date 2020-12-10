using UnityEngine;
using Mirror;

public class PickupObject : NetworkBehaviour
{
    [SerializeField]
    private GameObject pickupText;
    private GameObject collidingItem;

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
    }
    private void OnTriggerEnter(Collider other)
    {
        collidingItem = other.gameObject;
        pickupText.SetActive(true);
        
    }
    private void OnTriggerExit(Collider other)
    {
        collidingItem = null;
        pickupText.SetActive(false);
    }
    [Command]
    void CmdAddHealth(int amount)
    {
        RpcAddHealth(amount);
    }
    [Command]
    void CmdAddAmmo(int index,int amount)
    {
        RpcAddAmmo(index, amount);
    }

    [ClientRpc]
    void RpcAddHealth(int amount)
    {
        Health player_health = GetComponent<Health>();
        bool active = player_health.RestoreHP(amount);
        if (!active)
        {
            Destroy(collidingItem);
            pickupText.SetActive(false);
        }
    }
    [ClientRpc]
    void RpcAddAmmo(int index, int amount)
    {
        Ammunition player_ammunition = GetComponent<Ammunition>();
        player_ammunition.AddAmmunition(index, amount);
        Destroy(collidingItem);
        pickupText.SetActive(false);
    }
}
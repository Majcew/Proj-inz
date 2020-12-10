using UnityEngine;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour
{
    [SerializeField]
    private GameObject pickupText;
    private GameObject collidingItem;

   void Update() {
        if (Input.GetKeyDown("f") && collidingItem != null)
        {
            switch (collidingItem.tag)
            {
                case "health":
                    //CmdAddHealth();
                    AddHealth(collidingItem.GetComponent<HealthBox>().restoreHealth);
                    break;
                case "ammunition":
                    //CmdAddAmmo();
                    AddAmmo(collidingItem.GetComponent<PickupableAmmo>().index, collidingItem.GetComponent<PickupableAmmo>().amount);
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

    void AddHealth(int amount)
    {
        Health player_health = GetComponent<Health>();
        bool active = player_health.RestoreHP(amount);
        if (!active) { 
            Destroy(collidingItem);
            pickupText.SetActive(false);
        }
    }

    void AddAmmo(int index,int amount)
    {
        Ammunition player_ammunition = GetComponent<Ammunition>();
        player_ammunition.AddAmmunition(index, amount);
        Destroy(collidingItem);
        pickupText.SetActive(false);
    }
}
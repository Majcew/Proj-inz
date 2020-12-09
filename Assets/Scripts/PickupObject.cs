using UnityEngine;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour
{
    [SerializeField]
    private GameObject pickupText;

   void Update() {
        if (Input.GetKeyDown("e"))
        {
            // ObjectIwantToPickUp.GetComponent<Transform>();
        }
    }
    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.CompareTag("health") || other.gameObject.CompareTag("ammunition")) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            //TODO: Add script to add health or ammunition to the player
            pickupText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        pickupText.SetActive(false);
    }
}
using UnityEngine;

public class AddItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO: GameManager skrypt tutaj, oraz zniszcz przedmiot
            GameManager.AddItemCount();
            Destroy(this.gameObject);
        }
    }
}

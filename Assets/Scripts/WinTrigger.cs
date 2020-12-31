using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class WinTrigger : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("you win");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().ServerChangeScene("WinnerScene");
        }
    }

  
}

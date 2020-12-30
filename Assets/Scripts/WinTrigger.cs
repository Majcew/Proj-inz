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
            PlayerPrefs.SetInt("exit_scene", 4);
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopClient();
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopHost();
            PlayerPrefs.DeleteKey("exit_scene");
            SceneManager.LoadScene(4);
        }
    }

  
}

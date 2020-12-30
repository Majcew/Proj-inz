using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewManager : NetworkBehaviour
{
    [SerializeField]
    private Text itemsCountText;
    [SerializeField]
    private GameObject keyImage;
    [SyncVar]
    private int itemCount = GameManager.GetItemCount();
    [SyncVar]
    private bool keyState = GameManager.GetKeyState();

    public void SetItemCountText()
    {
        itemCount++;
        Debug.Log("PVM dziala");
        itemsCountText.text = itemCount + "/6";
    }
    public void SetKeyStateImage()
    {
        keyState = true;
        keyImage.SetActive(true);
    }
}

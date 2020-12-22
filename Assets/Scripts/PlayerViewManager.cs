using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewManager : NetworkBehaviour
{
    [SerializeField]
    private Text itemsCountText;
    [SyncVar]
    private int itemCount = GameManager.GetItemCount();

    public void SetItemCountText(int count)
    {
        itemCount++;
        Debug.Log("PVM dziala");
        itemsCountText.text = itemCount + "/6";
    }
}

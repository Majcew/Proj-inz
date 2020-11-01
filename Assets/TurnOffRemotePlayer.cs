using Mirror;
using UnityEngine;
using UnityEngine.Networking;

public class TurnOffRemotePlayer : NetworkBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        string id = string.Format("{0}", this.netId);
        Player_Movement scr = this.GetComponent<Player_Movement>();
        Camera cam = this.GetComponentInChildren< Camera >();

        if (this.isLocalPlayer)
        {
            cam.enabled = true;
            scr.enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            cam.enabled = false;
            scr.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

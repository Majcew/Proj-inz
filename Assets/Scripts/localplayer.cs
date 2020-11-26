using Mirror;
using UnityEngine;

public class localplayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.GetComponentInParent<NetworkIdentity>().isLocalPlayer) { 
            this.enabled = true;
        }
        else
        {
            this.enabled = false;
        }
    }
}

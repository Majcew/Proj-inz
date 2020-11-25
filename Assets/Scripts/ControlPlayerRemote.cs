using Mirror;
using UnityEngine;
using UnityEngine.Networking;

public class ControlPlayerRemote : NetworkBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        string id = string.Format("{0}", this.netId);
        Player_Movement scr = this.GetComponent<Player_Movement>();
        Camera cam = this.GetComponentInChildren<Camera>();
        GameObject wepcam = GameObject.Find("WeaponCamera");

        if (this.isLocalPlayer)
        {
            wepcam.SetActive(true);
            cam.enabled = true;
            scr.enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            wepcam.SetActive(false);
            cam.enabled = false;
            scr.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        /*string id = string.Format("{0}", this.netId);
        Player_Movement scr = this.GetComponent<Player_Movement>();
        Health health = this.GetComponent<Health>();
        Weapons wpn = this.GetComponent<Weapons>();
        Ammunition ammo = this.GetComponent<Ammunition>();
        Animator anim = this.GetComponent<Animator>();
        Camera cam = this.GetComponentInChildren<Camera>();
        GameObject gui = GameObject.Find("UI");
        GameObject wepcam = GameObject.Find("WeaponCamera");
        GameObject modelwep = GameObject.Find("PlayerModelWeapons");
        GameObject model = GameObject.Find("Geo");

        if (this.isLocalPlayer)
        {
            wepcam.SetActive(true);
            modelwep.layer = 9;
            model.layer = 9;
            gui.SetActive(true);
            cam.enabled = true;
            scr.enabled = true;
            health.enabled = true;
            wpn.enabled = true;
            ammo.enabled = true;
            anim.enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            wepcam.SetActive(false);
            gui.SetActive(false);
            ammo.enabled = false;
            health.enabled = false;
            cam.enabled = false;
            scr.enabled = false;
            wpn.enabled = false;
            anim.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }*/
    }
}

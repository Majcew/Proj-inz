using Mirror;
using System;
using UnityEngine;

public class ControlPlayerRemote : NetworkBehaviour
{
     public Behaviour[] all;
    // Start is called before the first frame update
    private void Start()
    {
        //Camera cam = this.GetComponentInChildren<Camera>();
       
        //String[] allowed = ["Mirror.TelepathyTransport"];

        if (!this.isLocalPlayer)
        {
            foreach(Behaviour i in all){
            Debug.Log("comp : " + i + "type : " + i.GetType());
               //if(i.GetType().ToString() != "Mirror.TelepathyTransport" && i.GetType().ToString() != "Mirror.NetworkIdentity" && i.GetType().ToString() != "Mirror.NetworkTransform"){
                    i.enabled = false;
                //}else{
               // Debug.Log("tele skiped!!");}
               
            }
           // cam.enabled = true;
        } 

        /*string id = string.Format("{0}", this.netId);
        Health health = this.GetComponent<Health>();
        Weapons wpn = this.GetComponent<Weapons>();
        Ammunition ammo = this.GetComponent<Ammunition>();
        Camera cam = this.GetComponentInChildren<Camera>();
        GameObject gui = GameObject.Find("UI");
        GameObject wepcam = GameObject.Find("WeaponCamera");

        if (this.isLocalPlayer)
        {
            wepcam.SetActive(true);
            gui.SetActive(true);
            cam.enabled = true;
            health.enabled = true;
            wpn.enabled = true;
            ammo.enabled = true;
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
            wpn.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }*/
    }
}

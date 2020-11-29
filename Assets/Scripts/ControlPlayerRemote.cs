﻿using Mirror;
using System;
using UnityEngine;

public class ControlPlayerRemote : NetworkBehaviour
{
    public Behaviour[] all;

    private void Start()
    {

        if (!this.isLocalPlayer)
        {
            foreach(Behaviour i in all){
                    i.enabled = false;   
            }
        } 
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        GameManager.AddPlayerHealth(this.netId.ToString(), this.GetComponent<Health>());
    }

    void OnDisable()
    {
        GameManager.RemovePlayerHealth(this.netId.ToString());
    }
}

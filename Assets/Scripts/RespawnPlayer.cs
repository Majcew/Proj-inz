using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] behaviourComponents;
    [SerializeField]
    private CharacterController cc;
    [SerializeField]
    private Ammunition ammunition;
    [SerializeField]
    private Weapons weapons;
    private bool[] states;

    public void Setup()
    {
        states = new bool[behaviourComponents.Length + 1];
        for (int i = 0; i < behaviourComponents.Length; i++)
        {
            states[i] = behaviourComponents[i].enabled;
        }
        states[behaviourComponents.Length] = cc.enabled;

        GameManager.GetPlayerHealth(this.netId.ToString()).CmdResetHealth();
        ammunition.ResetAmmunition();
        weapons.ResetWepon();
    }

    public void disableBehaviourComponents()
    {
        foreach (Behaviour bc in behaviourComponents)
        {
            bc.enabled = false;
        }

        cc.enabled = false;
    }

    public void enableBehaviourComponents()
    {
        foreach (Behaviour bc in behaviourComponents)
        {
            bc.enabled = true;
        }

        cc.enabled = true;

        GameManager.GetPlayerHealth(this.netId.ToString()).CmdResetHealth();
        ammunition.ResetAmmunition();
        weapons.ResetWepon();
    }
}

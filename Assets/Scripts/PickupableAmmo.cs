using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableAmmo : MonoBehaviour
{
    [SerializeField]
    private int _index;
    [SerializeField]
    private int _amount;
    [HideInInspector]
    public int index;
    [HideInInspector]
    public int amount;
    // Start is called before the first frame update
    void Start()
    {
        index = _index;
        amount = _amount;
    }
}

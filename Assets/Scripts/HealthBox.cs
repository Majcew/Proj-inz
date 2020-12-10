using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [SerializeField]
    private int _restoreHealth = 20;
    [HideInInspector]
    public int restoreHealth;
    // Start is called before the first frame update
    void Start()
    {
        restoreHealth = _restoreHealth;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMenuTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("info: " + PlayerPrefs.GetString("host_name"));
        Debug.Log("mode: " + PlayerPrefs.GetString("mode"));
        string title = "Title";

        switch (PlayerPrefs.GetInt("scene_id"))
        {
            case 3 : 
                title = "PVP";
                break;
            default:
                title = "Multiplayer options";
                break;
        }

       GetComponent<Text>().text = title;
    }

}

using UnityEngine;
using System;

public class ClimbLadder : MonoBehaviour
{
    #region Variables
    public GameObject playerOBJ;
    public bool canClimb = false;
    public float speed = 1;
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canClimb = true;
            playerOBJ = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canClimb = false;
            playerOBJ = null;
        }
    }
    void Update()
    {
        if (canClimb)
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerOBJ.transform.Translate(Vector3.up * Time.deltaTime * speed);
                Console.WriteLine(Vector3.up * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerOBJ.transform.Translate(Vector3.down * Time.deltaTime * speed);
                Console.WriteLine(Vector3.down * Time.deltaTime * speed);
            }
        }
    }
}

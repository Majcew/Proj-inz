using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractions : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        CloseDoor();
    }

    public void OpenDoor()
    {
        animator.SetBool("Open", true);
    }
    public void CloseDoor()
    {
        animator.SetBool("Open", false);
    }
}

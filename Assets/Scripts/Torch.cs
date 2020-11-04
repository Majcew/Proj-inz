using UnityEngine;

public class Torch : MonoBehaviour
{

    [Header("Add the torch object")]
    public GameObject torch;
    [Header ("The state of the object (true if in use)")]
    public bool isActive;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0) && !isActive)
        {
            torch.SetActive(true);
            isActive = true;
        }else if(Input.GetKeyDown(KeyCode.Alpha0) && isActive)
        {
            torch.SetActive(false);
            isActive = false;
        }
    }
}

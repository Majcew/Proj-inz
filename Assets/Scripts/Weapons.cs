using UnityEngine;

public class Weapons : MonoBehaviour
{
    [Header("Add the weapons")]
    public GameObject[] weaponobj;
    [Header("The state of the weapons")]
    public bool[] isActive;

    private void Awake()
    {
        foreach (GameObject obj in weaponobj)
        {
            obj.SetActive(false);
        }
        foreach (bool value in isActive)
        {
            value.Equals(false);
        }
        weaponobj[1].SetActive(true);
        isActive[1] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isActive[0])
        {
            ImportantFunction();
            weaponobj[0].SetActive(true);
            isActive[0] = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !isActive[1])
        {
            ImportantFunction();
            weaponobj[1].SetActive(true);
            isActive[1] = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0) && !isActive[2])
        {
            ImportantFunction();
            weaponobj[2].SetActive(true);
            isActive[2] = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !isActive[3])
        {
            ImportantFunction();
            weaponobj[3].SetActive(true);
            isActive[3] = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !isActive[4])
        {
            ImportantFunction();
            weaponobj[4].SetActive(true);
            isActive[4] = true;
        }
    }
    void ImportantFunction()
    {
        foreach (GameObject elem in weaponobj)
        {
            elem.SetActive(false);
        }
        for (int i = 0; i < isActive.Length; i++)
        {
            isActive[i] = false;
        }
    }
}
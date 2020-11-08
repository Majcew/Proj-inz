using UnityEngine;
using UnityEngine.UI;

public class HideAmmunitionText : MonoBehaviour
{
    //public Text ammoinmagText;
    //public Text overallammoText;
    public GameObject ammoInd;

    private void OnDisable()
    {
        HideUIInformations(true);
    }
    private void OnEnable()
    {
        HideUIInformations(false);
    }
    private void HideUIInformations(bool state)
    {
        //overallammoText.gameObject.SetActive(state);
        //ammoinmagText.gameObject.SetActive(state);
        ammoInd.SetActive(state);
    }

}

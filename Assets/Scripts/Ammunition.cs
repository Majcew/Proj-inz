using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Ammunition : NetworkBehaviour
{
    [Header("Limit the amount of ammunition in the magazine")]
    public int[] bulletsPerMag;
    [Header("The maximum ammo capacity to the weapon")]
    public int[] maxAmmoOverall;
    [Header("Current amount ammo in mag")]
    public int[] bulletsInMag;
    [Header("Current amount of ammo left('not counting the one in the magazine')")]
    public int[] bulletsLeft;
    public Text ammoinmagText;
    public Text overallammoText;

    public void ShowAmmoLeft(int id)
    {
        overallammoText.text = bulletsLeft[id].ToString();
    }

    public void ShowAmmoInMag(int id)
    {
        ammoinmagText.text = bulletsInMag[id].ToString();
    }

    public void AddAmmunition(int id, int value)
    {
        bulletsLeft[id] += value;
        updateUIText(id);
    }
    public void ResetAmmunition()
    {
        bulletsPerMag.CopyTo(bulletsInMag, 0);
        maxAmmoOverall.CopyTo(bulletsLeft, 0);
    }
    private void updateUIText(int id)
    {
        overallammoText.text = bulletsLeft[id].ToString();
        ammoinmagText.text = bulletsInMag[id].ToString();
    }
}
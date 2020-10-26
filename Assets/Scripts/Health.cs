using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Variables
    public float maxHealth;

    float currentHealth;
    bool isAlive = true;
    #endregion

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float value)
    {
        if (isAlive)
        {
            currentHealth -= value;
        }
        if (currentHealth <= 0)
        {
            isAlive = false;
            currentHealth = 0; // Dla gui nie chcemy ujemnych wartości, dlatego tutaj jest 0
        }
    }

    private void Death()
    {
        /* 
            Instrukcja odpowiedzialna za śmierć postaci/przeciwnika.
         */
    }

    public void HealHealth(float value)
    {
        // Procedura unikająca uleczenie postaci ponad max HP.
        if(currentHealth + value < maxHealth)
        {
            currentHealth += value;
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    private void LateUpdate()
    {
        // Sprawdzamy co fps czy postać żyje. Jeżeli nie to wywołujemy procedurę śmierci.
        if (!isAlive) Death();
    }
}

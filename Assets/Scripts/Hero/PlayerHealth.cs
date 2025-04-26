using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using TMPro;
=======
>>>>>>> 6d943f248c7a1e04eb1148b551a6b81578dbd9a3
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

<<<<<<< HEAD
    public TMP_Text healthText;

    void Start()
    {
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;
=======
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
>>>>>>> 6d943f248c7a1e04eb1148b551a6b81578dbd9a3

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

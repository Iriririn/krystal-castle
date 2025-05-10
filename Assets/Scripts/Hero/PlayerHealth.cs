using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public PlayerHealthBar healthBar;

    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthBar.SetHealth(currentHealth);
    }

    //public void PlayerDeath()
    //{
        //if(currentHealth <= 0)
        //{
            //gameObject.SetActive(false); //change to anim once sprite is finished
            //FindFirstObjectByType<GameController>().EndGame();
        //}
    //}

    void FixedUpdate()
    {
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false); //change to anim once sprite is finished
            FindFirstObjectByType<GameController>().EndGame();
        }
    }
}

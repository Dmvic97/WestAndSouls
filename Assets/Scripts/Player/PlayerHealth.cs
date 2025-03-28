using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
  
    

    public HealthUI healthUI;
    public static event Action OnPlayerDied;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
    }

    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);
        
        
        if (currentHealth > 0)
        {
            //player hurt
        }
        else
        {
            GetComponent<PlayerMovement>().rb.linearVelocity = new Vector2 (0, 0); //Dejamos al personaje quieto
            GetComponent<PlayerMovement>().animator.SetTrigger("Dead");
            GetComponent<PlayerMovement>().enabled = false; //Impedimos que se mueva quieto

            OnPlayerDied?.Invoke(); //Verificamos que el evento no sea null
        }
    }
}

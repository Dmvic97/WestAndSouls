using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    
    [HideInInspector]
    public int currentHealth;
    public bool isDead;

    [SerializeField] AudioManager audioManager;

    public HealthUI healthUI;
    public static event Action OnPlayerDied;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
        isDead = false;
    }

    

    public void TakeDamage(int damage)
    {
        audioManager.PlaySFX(audioManager.playerHurt);
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);
        
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    /*Se trato de implementar un objeto de curacion pero no conseguí que fucnionara, 
     * entiendo que por un problema de colisiones
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        healthUI.UpdateHearts(currentHealth);
    }
    */

    void Die()
    {
        isDead = true;
        gameObject.layer = LayerMask.NameToLayer("DeadPlayer"); //Como el script de enemy ataca segun layer,al cambiar el layer dejara de atacar
        GetComponent<Player>().rb.linearVelocity = new Vector2(0, 0); //Dejamos al personaje quieto
        GetComponent<Player>().animator.SetTrigger("Dead");
        GetComponent<Player>().enabled = false; //Impedimos que se mueva quieto
        audioManager.PlaySFX(audioManager.death);

        audioManager.PlaySFX(audioManager.gameOverScreen);
        OnPlayerDied?.Invoke(); //Verificamos que el evento no sea null
    }
}

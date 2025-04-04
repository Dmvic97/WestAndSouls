using UnityEngine;

public class HealItem : MonoBehaviour
{
    /* No he conseguido que funcione
    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Asegúrate de que colisiona con el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha tocado el objeto curativo.");

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                Debug.Log("Curando jugador por " + healAmount);
                playerHealth.Heal(healAmount);
            }
            else
            {
                Debug.LogWarning("No se encontró PlayerHealth en el objeto Player.");
            }

            Destroy(gameObject);
        }
    }
    */
}

using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public int bulletDamage = 1;
    Animator anim;
    public AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindWithTag("Audio")?.GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //float direction = Mathf.Sign(transform.localScale.x); //funcion mat. que tranforma el valor en negativo si es positivo y viceversa
                                                              //lo utilizamos para cambiar la direccion de la bala
                                                              //principalmente cogemos el valor del scale de la bala cambiado y lo multiplicamos
                                                              //por speed para que vaya en la direccion que corresponda
                                                              
                                                              //funcionaba pero al tocar algo dejo de funcionar

        float direction = transform.lossyScale.x > 0 ? 1 : -1;
        rb.linearVelocity = new Vector2(speed * direction, 0);
        
    }

    void Update()
    {
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("MainCamera")) //Tenemos un colider para mantener la camara en los limites
                                                         //debemos hacer que la bala ignore esta colision
        {
            if (collision.CompareTag("Player")) // En caso de que tenga el tag Player
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                if (playerHealth != null) // Evita error null
                {
                    playerHealth.TakeDamage(bulletDamage);
                    audioManager.PlaySFX(audioManager.bulletImpact);
                }

            }
            if (!collision.CompareTag("enemy")) //Quiero que las balas de enemigos no se destruyan con los propios enemigos
                                                //se debería poder añadir esta condición junto con el primer if, pero no funcionaba correctamente
            {
                anim.SetTrigger("Impact");
                audioManager.PlaySFX(audioManager.bulletImpactGround);
                Destroy(gameObject, 0.1f);
            }
            
        }
            

    }

    void DestroyBullet()
    {
        Destroy(gameObject, 5f);
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public int bulletDamage = 1;
    Animator anim;
    private AudioManager audioManager;

    // Update is called once per frame
    void Start()
    {
        audioManager = GameObject.FindWithTag("Audio")?.GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
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
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy) 
            {
                enemy.TakeDamage(bulletDamage);
                audioManager.PlaySFX(audioManager.bulletImpact);

            }
            rb.linearVelocity = new Vector2(0, 0);
            anim.SetTrigger("Impact");
            audioManager.PlaySFX(audioManager.bulletImpactGround);
            Destroy(gameObject,0.1f);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject, 5f);
    }
    
}

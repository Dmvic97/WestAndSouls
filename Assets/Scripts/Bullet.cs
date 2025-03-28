using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public int bulletDamage = 1;

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
    }

    void Update()
    {
        DestroyBullet();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy) 
        {
            enemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject, 15f);
    }
    
}

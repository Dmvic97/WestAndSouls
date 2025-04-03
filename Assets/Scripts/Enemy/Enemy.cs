using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Range Attack")]
    [SerializeField] private Transform enemyShootingPoint;
    [SerializeField] private GameObject enemyBulletPrefab;


    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("References")]
    [SerializeField] private Animator anim;
    [SerializeField] AudioManager audioManager;

    [Header("Taking Damage")]
    public int maxHealth = 1;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;

    private EnemyPatrol enemyPatrol; //Vamos a utilizar una variable con herencia del cs enemyPatrol
                                     //para detener la patrulla cuando el enemigo nos detecte

    private void Awake()
    {
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {

        cooldownTimer += Time.deltaTime;

        //Atacar unicamente si el jugador esta en su rango de ataque
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                RangedAttack();
            }
        }
        
        
        if (enemyPatrol != null) 
            enemyPatrol.enabled = !PlayerInSight();//La patrulla del enemigo solo estara activa cuando no estemos en su rango
            }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        GameObject bullet = Instantiate(enemyBulletPrefab, enemyShootingPoint.position, Quaternion.identity);//guardamos el prefab sobre un gameobject para poder modificar su escala
        bullet.transform.localScale = new Vector3(transform.localScale.x > 0 ? 1 : -1, 1, 1);
        anim.SetTrigger("Shoot");
        audioManager.PlaySFX(audioManager.enemyShot);


        if (Camera.main.WorldToViewportPoint(enemyShootingPoint.position).x > 1 ||
        Camera.main.WorldToViewportPoint(enemyShootingPoint.position).x < 0)
        {
            Debug.LogWarning("FirePoint está fuera de la vista de la cámara");//Evita que crashee el error por este problema
                                                                              //no se exactamente porque esta fuera
        }
        
    }

    private bool PlayerInSight()
    {   //""complejo sistema de ecuaciones"" que indica la distancia a la que atacará el enemigo
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0, Vector2.left, 0, playerLayer);


        return hit.collider != null;
    }

    //Para visualizar el rango
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
   

    void Die()
    {
        anim.SetTrigger("Death");
        GetComponent<Rigidbody2D>().simulated = false;//Desactivo las fisicas -> No interactua con las balas ni el jugador mas
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = false;//Desactivamos el script de patrulla
        }

        audioManager.PlaySFX(audioManager.enemyDeath);
        cooldownTimer = Mathf.Infinity; //Desctivamos el tiro
        this.enabled = false; //Desactivamos el propio script
    }
}



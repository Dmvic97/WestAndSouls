using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    bool isFacingRight = true;
    public Animator animator;

    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Fire")]
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public float attackCooldown;
    

    [Header("Gravity")]
    public float baseGravity = 2;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [Header("Respawn")]
    public float fallLimit = -2f; //Altura a la que efectuaremos el respawn
    private Vector2 lastPosition; //La ultima posicion en la que se encontraba el jugador antes de caer
    public LayerMask spawnerLayer;

    AudioManager audioManager;
    private float walkInterval = 0.5f; // Para que los pasos se escuchen correctamente tenemos que hacer que suenen cada cierto tiempo,
                                       // para ello añadimos un valor para el intervalo de tiempo y un temporizador
    private float walkTimer = 0f;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        lastPosition = transform.position; //Guardamos la posicion inicial al empezar la partida, entiendo que para evitar errores
    }
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        Gravity();
        Flip();
        if (Input.GetKeyDown(KeyCode.J))
        {
            Fire();
        }
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        if(IsGrounded() == true && horizontalMovement != 0)
        {
            walkTimer -= Time.deltaTime;
            if (walkTimer <= 0f)
            {
                audioManager.PlaySFX(audioManager.walk);
                walkTimer = walkInterval;
            }
        }
        
        if (transform.position.y < fallLimit)
        {
            Respawn();
        }
    }

    private void Gravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; //Usamos la gravedad para afectar a la caída y que no se sienta como que el pj flota
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed)); //Limita la máxima velocidad de caída
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x; //Forma en que detecte el movimiento en el eje x
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded()) // Solo salta si está en el suelo
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            animator.SetTrigger("Jump");
            audioManager.PlaySFX(audioManager.jump);
        }

        if (context.canceled && rb.linearVelocity.y > 0) // Si suelta el botón y sigue subiendo
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f); // Reduce la altura del salto
        }
    }

    private bool IsGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            return true;
            
        }
        return false; 
    }

    private void Flip()
    {
        if ( isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    void Fire()
    {
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        animator.SetTrigger("Shot");
        audioManager.PlaySFX(audioManager.shot);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spawn"))
        {
            lastPosition = transform.position;
            Debug.LogWarning("Detecta spawn");
        }
    }
    public void Respawn()
    {
        GetComponent<PlayerHealth>().TakeDamage(1);
        //if (GetComponent<PlayerHealth>().currentHealth <= 0) //Si esta muerto no se hace respawn
        //{
        /*
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, spawnerLayer))
        {
            lastPosition = transform.position;
            Debug.LogWarning("Detecta spawn");
        }
        */
        transform.position = lastPosition;
        rb.linearVelocity = Vector2.zero;
        //} Me mata directamente y encima respawnea D:
            
    }
    
}

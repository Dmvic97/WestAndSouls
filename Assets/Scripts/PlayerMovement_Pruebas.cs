using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_Pruebas : MonoBehaviour
{
    
    // Movement variables
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    private float horizontalMovement;

    // Jump variables
    public float jumpPower = 10f; // La altura m�xima del salto
    private float jumpTime = 0f;  // Tiempo que se ha mantenido presionado el salto
    private bool isJumping = false; // Determina si el jugador est� saltando
    private bool jumpButtonHeld = false; // Determina si el bot�n de salto est� siendo mantenido presionado
    private float initialJumpPower; // Para almacenar el poder inicial del salto (salto corto)

    void Start()
    {
        initialJumpPower = jumpPower / 2; // Asignamos la mitad del valor de jumpPower como salto corto
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);

        // Si el bot�n de salto est� siendo mantenido presionado, incrementar el tiempo de salto
        if (jumpButtonHeld && rb.linearVelocity.y > 0)
        {
            jumpTime += Time.deltaTime; // Aumentar el tiempo que se mantiene presionado el bot�n
        }
    }

    // Control de movimiento horizontal
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x; // Detecta el movimiento en el eje X
    }

    // Control del salto
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && rb.linearVelocity.y == 0) // Salto solo si est� tocando el suelo (sin velocidad en Y)
        {
            isJumping = true;
            jumpButtonHeld = true; // El bot�n ha sido presionado
            jumpTime = 0f; // Reinicia el tiempo de salto
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, initialJumpPower); // Salto corto (la mitad de la altura)
        }
        else if (context.canceled && isJumping)
        {
            // Se suelta el bot�n de salto, terminamos de ajustar el salto
            jumpButtonHeld = false;
        }
    }

    // Control de la duraci�n del salto seg�n el tiempo de presionado de la tecla
    private void FixedUpdate()
    {
        if (isJumping && rb.linearVelocity.y > 0) // Mientras est� en el aire y subiendo
        {
            // Ajustamos la velocidad del salto para que llegue a la altura m�xima si se mantiene presionado
            if (jumpButtonHeld)
            {
                // Si se mantiene presionado el bot�n de salto, aumentamos la velocidad para llegar a la altura m�xima
                float targetJumpPower = Mathf.Lerp(initialJumpPower, jumpPower, jumpTime);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, targetJumpPower);
            }
        }
    }
}
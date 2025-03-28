using UnityEngine;

public class PhantomPlatform : MonoBehaviour
{
    private Collider2D platformCollider2D;
    private Renderer platformRenderer;

    void Start()
    {
        platformCollider2D = GetComponent<Collider2D>();
        platformRenderer = GetComponent<Renderer>();
        platformCollider2D.enabled = false; // Inicialmente sin colisión
        SetTransparency(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Activar colisión en tiempo bala
        {
            EnableCollision();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // Desactivar colisión al salir de tiempo bala
        {
            DisableCollision();
        }
    }

    void EnableCollision()
    {
        platformCollider2D.enabled = true;
        SetTransparency(false);
    }

    void DisableCollision()
    {
        platformCollider2D.enabled = false;
        SetTransparency(true);
    }

    void SetTransparency(bool isTransparent)
    {
        if (platformRenderer != null)
        {
            Color color = platformRenderer.material.color;
            color.a = isTransparent ? 0.3f : 1f; // Transparencia del 30% si está desactivada
            platformRenderer.material.color = color;
        }
    }
}


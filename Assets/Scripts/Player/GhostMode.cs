/*
using UnityEngine;

public class GhostMode : MonoBehaviour
{
    public float worldSlowMotionFactor = 0.2f; // Cuánto queremos ralentizar el mundo
    public float playerSpeedFactor = 1.5f; // Factor de velocidad del jugador en tiempo bala
    public float slowMotionDuration = 2f; // Duración máxima del tiempo bala

    public float maxGhostModeDuration = 5f;
    
    [HideInInspector] //permite hacer la variable publica sin que se muestre en el inspector
    public float currentGhostModeDuration;
    
    public float drainRate = 1f; // Consumo de slow motion por segundo
    public float rechargeRate = 0.5f; // Recarga de slow motion por segundo

    private float normalTimeScale = 1f;
    private float normalPlayerSpeed = 1f;
    private bool isGhostModeActive = false;
    private PlayerMovement playerController;
    void Start()
    {
        playerController = Object.FindFirstObjectByType<PlayerMovement>(); 
        if (playerController != null)
        {
            normalPlayerSpeed = playerController.moveSpeed;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Activa el tiempo bala con Shift Izquierdo
        {
            ActivateGhostMode();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // Desactiva el tiempo bala al soltar la tecla
        {
            DeactivateGhostMode();
        }
    }

    void ActivateGhostMode()
    {
        if (!isGhostModeActive)
        {
            isGhostModeActive = true;
            Time.timeScale = worldSlowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajusta la física al nuevo tiempo
            if (playerController != null)
            {
                playerController.moveSpeed = normalPlayerSpeed * playerSpeedFactor;
            }
            StartCoroutine(DisableGhostModeAfterDuration());
        }
    }

    void DeactivateGhostMode()
    {
        isGhostModeActive = false;
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        if (playerController != null)
        {
            playerController.moveSpeed = normalPlayerSpeed;
        }
    }

    System.Collections.IEnumerator DisableGhostModeAfterDuration()
    {
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        DeactivateGhostMode();
    }
}
*/
using UnityEngine;

public class GhostMode : MonoBehaviour
{
    public float worldSlowMotionFactor = 0.2f; // Cuánto ralentizamos el mundo
    public float playerSpeedFactor = 1.5f; // Factor de velocidad del jugador en slow motion

    // Nueva configuración para la duración del slow motion
    public float maxGhostModeDuration = 5f; // Máximo tiempo de slow motion
    public float drainRate = 1f; // Consumo de slow motion por segundo
    public float rechargeRate = 0.5f; // Recarga de slow motion por segundo

    private float normalTimeScale = 1f;
    private float normalPlayerSpeed = 1f;
    private bool isGhostModeActive = false;
    private PlayerMovement playerController; // Referencia al script del jugador

    public GhostBarCharge ghostBarCharge; // Nueva referencia a la barra de recursos

    [HideInInspector]
    public float currentGhostModeDuration; // Variable añadida para rastrear la duración del slow motion

    void Start()
    {
        playerController = Object.FindFirstObjectByType<PlayerMovement>(); // Encuentra el script del jugador
        if (playerController != null)
        {
            normalPlayerSpeed = playerController.moveSpeed;
        }

        currentGhostModeDuration = maxGhostModeDuration; // Inicializa con el valor máximo
    }

    void Update()
    {
        // Modificado: Ahora el slow motion solo se activa si hay recurso disponible
        if (Input.GetKey(KeyCode.LeftShift) && currentGhostModeDuration > 0)
        {
            ActivateGhostMode();
            currentGhostModeDuration -= drainRate * Time.unscaledDeltaTime; // Se gasta mientras está activo
        }
        else
        {
            DeactivateGhostMode();
            currentGhostModeDuration += rechargeRate * Time.deltaTime; // Se recarga cuando no está activo
        }

        currentGhostModeDuration = Mathf.Clamp(currentGhostModeDuration, 0, maxGhostModeDuration); // Evita valores fuera del rango
    }

    void ActivateGhostMode()
    {
        if (!isGhostModeActive)
        {
            isGhostModeActive = true;
            Time.timeScale = worldSlowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            if (playerController != null)
            {
                playerController.moveSpeed = normalPlayerSpeed * playerSpeedFactor;
            }
        }
    }

    void DeactivateGhostMode()
    {
        if (isGhostModeActive)
        {
            isGhostModeActive = false;
            Time.timeScale = normalTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            if (playerController != null)
            {
                playerController.moveSpeed = normalPlayerSpeed;
            }
        }
    }
}



using UnityEngine;
using UnityEngine.Rendering;

public class GhostMode : MonoBehaviour
{
    [Header("Movement Stats")]
    public float worldSlowMotionFactor = 0.2f; // Cuánto ralentizamos el mundo
    public float playerSpeedFactor = 1.5f; // Factor de velocidad del jugador en slow motion
    public float jumpPowerFactor = 1.2f; // Factor para salto
    public float gravityFactor = 0.5f; // Factor para gravedad

    [Header("Time Stats")]
    public float maxGhostModeDuration = 5f; // Máximo tiempo
    public float drainRate = 1f; // Consumo por segundo
    public float rechargeRate = 0.5f; // Recarga por segundo

    private float normalTimeScale = 1f;
    private float normalPlayerSpeed;
    private float normalJumpPower;
    private float normalBaseGravity;
    private float normalFallSpeedMultiplier;
    private bool isGhostModeActive = false;
    

    [Header("References")]
    public GhostBarCharge ghostBarCharge; 
    public Volume postProcessVolume;
    private PlayerMovement playerController;

    [HideInInspector]
    public float currentGhostModeDuration; // Determina la carga restante del recurso

    void Start()
    {
        playerController = Object.FindFirstObjectByType<PlayerMovement>(); // Encuentra el script del jugador
        if (playerController != null)
        {
            normalPlayerSpeed = playerController.moveSpeed;
            normalJumpPower = playerController.jumpPower;
            normalBaseGravity = playerController.baseGravity;
            normalFallSpeedMultiplier = playerController.fallSpeedMultiplier;
        }

        currentGhostModeDuration = maxGhostModeDuration; // Inicializa con el valor máximo
    }

    void Update()
    {
        //Se activa aprentando la tecla solo si hay recurso disponible
        if (Input.GetKey(KeyCode.LeftShift) && currentGhostModeDuration > 0)
        {
            ActivateGhostMode();
            currentGhostModeDuration -= drainRate * Time.unscaledDeltaTime; // Se gasta mientras está activo
            postProcessVolume.enabled = true; //Activamos el efecto de postprocesado
        }
        else
        {
            DeactivateGhostMode();
            currentGhostModeDuration += rechargeRate * Time.deltaTime; // Se recarga cuando no está activo
            postProcessVolume.enabled = false; //Desactivamso el proceso de postprocesado
        }

        currentGhostModeDuration = Mathf.Clamp(currentGhostModeDuration, 0, maxGhostModeDuration); // Evita valores fuera del rango
    }

    void ActivateGhostMode()
    {
        if (!isGhostModeActive)
        {
            isGhostModeActive = true; //Activamos el ghost mode
            Time.timeScale = worldSlowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale; //Disminuimos el transcurso del tiempo
            if (playerController != null)
            {   //Multimplicamos varios factores para manejar la velocidad del personaje de forma paralela a la ralentizacion
                playerController.moveSpeed = normalPlayerSpeed * playerSpeedFactor;
                playerController.jumpPower = normalJumpPower * jumpPowerFactor;
                playerController.baseGravity = normalBaseGravity * gravityFactor;
                playerController.fallSpeedMultiplier = normalFallSpeedMultiplier * gravityFactor;
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
                playerController.jumpPower = normalJumpPower;
                playerController.baseGravity = normalBaseGravity;
                playerController.fallSpeedMultiplier = normalFallSpeedMultiplier;
            }
        }
    }
}
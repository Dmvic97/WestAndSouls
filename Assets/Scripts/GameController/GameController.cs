using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header ("Game Over")]
    public GameObject gameOverScreen;
    

    [Header("Pause")]
    public GameObject pauseScreen;
    void Start()
    {
        PlayerHealth.OnPlayerDied += GameOverScreen;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    
    private void Update()
    {
        //Pause
        if (Input.GetKeyDown(KeyCode.Escape)) //Detectamos la tecla esc
        {
            //Primero comprobamos si el menu de pausa ya esta activo
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);//Si está activo desactivamos desactivamos la pausa
            }
            else
            {
                PauseGame(true);
            }
            
        }
    }

    #region GameOverScreen
    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        
    }
    #endregion

    #region Reestart and loading scenes
    public void ResetGame()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    
    void OnDestroy()
    {
        PlayerHealth.OnPlayerDied -= GameOverScreen;
        //Antes al hacer retry habiendo muerto una vez al morir no volvía a salir el menu dado que ya había ocurrido el evento
        //Eliminamos el evento para que pueda volver a ocurrir
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        //Si el status es true pausamos el juego
        pauseScreen.SetActive(status);

        if (status)
            Time.timeScale = 0;

        else
            Time.timeScale = 1;
    }

    #endregion 
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject resetButton;
    void Start()
    {
        PlayerHealth.OnPlayerDied += GameOverScreen;
        gameOverScreen.SetActive(false);
    }
    /* Se ha movido a un nuevo script "Main Menu"
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    */

    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        
    }

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

}

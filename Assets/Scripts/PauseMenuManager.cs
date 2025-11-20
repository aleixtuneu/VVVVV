using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;

    private bool _isGamePaused = false;

    public void Start()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        // S'activa només quan es pressiona escape
        if (context.started)
        {
            if (_isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Pausar el joc
    public void PauseGame()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true); // Mostrar menu de pausa
            Debug.Log("PauseMenuManager: Panel de pausa activado.");
        }
        else
        {
            Debug.LogError("PauseMenuManager: ¡El 'Pause Menu Panel' no está asignado en el Inspector!");
        }

        Time.timeScale = 0f; // Aturar temps del joc
        _isGamePaused = true;
        Debug.Log("Juego Pausado.");
    }

    // Reanudar joc
    public void ResumeGame()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false); // Ocultar menu de pausa
        }
        Time.timeScale = 1f; // Reanudar temps
        _isGamePaused = false;
    }

    // Reiniciar partida
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reanudar temps abans de recarregar l'escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Carregar escena actual
    }

    // Anar al menú principal
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Reanudar temps abans de recarregar l'escena
        SceneManager.LoadScene("MainMenuScene"); // Carregar escena del menú principal
    }
}

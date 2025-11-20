using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject winMenuPanel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Si ja hi ha una instància, destruir el gameobject
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (winMenuPanel != null) winMenuPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    // Guanyar
    public void WinGame()
    {
        Debug.Log("¡Juego Ganado!");
        if (winMenuPanel != null)
        {
            winMenuPanel.SetActive(true); // Mostrar menú de victoria
        }
        Time.timeScale = 0f; // Pausar
    }

    // Perdre
    public void GameOver()
    {
        Debug.Log("¡Juego Perdido!");
        {
            if (winMenuPanel != null)
            {
                winMenuPanel.SetActive(true);
            }
        }
        Time.timeScale = 0f; // Pausar
    }

    // Restart
    public void RestartGame()
    {
        Debug.Log("Reiniciando partida...");
        Time.timeScale = 1f; // Reanudar temps
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarregar la escena actual
    }

    public void QuitToMainMenu()
    {
        Debug.Log("Saliendo al menú principal...");
        Time.timeScale = 1f; // Reanudar temps
        SceneManager.LoadScene("MainMenuScene"); // Carregar menú principal
    }
}

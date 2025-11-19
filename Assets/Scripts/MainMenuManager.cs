using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Botó Play
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Botó Exit
    public void QuitGame()
    {
        Application.Quit();
    }
}

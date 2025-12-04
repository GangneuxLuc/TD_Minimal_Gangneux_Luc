using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public string sceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


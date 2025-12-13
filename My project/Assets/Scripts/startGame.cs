using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour // Script pour démarrer le jeu ou quitter l'application via les boutons du menu principal
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


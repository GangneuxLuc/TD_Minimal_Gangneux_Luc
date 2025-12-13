using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] bool isPaused;
    [SerializeField] Button resume;
    [SerializeField] Button quit;
    int Hp;
    void Update() //Gère la pause du jeu et la détection de la mort du joueur
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
        OnPlayerDeath();
    }

    void OnPlayerDeath() //Vérifie si le joueur est mort et gère la fin du jeu
    {
       Hp = GameObject.FindWithTag("Player").GetComponent<PlayerProp>().HP;
        if (Hp <= 0)
        {
            Time.timeScale = 0f;
            Debug.Log("Player Died - Game Over");
           SceneManager.LoadScene("Title");
        }

    }
    void Pause() //Met le jeu en pause et affiche le menu de pause
    {
       
        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log("Game Paused");
        Instantiate(pauseMenu);
        resume = GameObject.Find("ResumeButton").GetComponent<Button>();
        resume.onClick.AddListener(ResumeButton);
        quit = GameObject.Find("QuitButton").GetComponent<Button>();
        quit.onClick.AddListener(QuitButton);
    }

    protected void Resume() //Reprend le jeu à partir de la pause
    {
       
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Game Resumed");
        Destroy(GameObject.Find(pauseMenu.name + "(Clone)"));
    }

    void ResumeButton() //Gère le clic sur le bouton de reprise
    {
        Debug.Log("Resume Button Clicked");
        Resume();
    }

    void QuitButton() //Gère le clic sur le bouton de quitter
    {
            Application.Quit();
    }
    
}

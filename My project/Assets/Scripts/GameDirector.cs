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

    private void Start()
    {
        
    }
    void Update()
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

    void OnPlayerDeath()
    {
       Hp = GameObject.FindWithTag("Player").GetComponent<PlayerProp>().HP;
        if (Hp <= 0)
        {
            Time.timeScale = 0f;
            Debug.Log("Player Died - Game Over");
           SceneManager.LoadScene("Title");
        }

    }
    void Pause()
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

    protected void Resume()
    {
       
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Game Resumed");
        Destroy(GameObject.Find(pauseMenu.name + "(Clone)"));
    }

    void ResumeButton()
    {
        Debug.Log("Resume Button Clicked");
        Resume();
    }

    void QuitButton()
    {
            Application.Quit();
    }
    
}

using UnityEditor;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] bool isPaused;
  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {

        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log("Game Paused");
        Instantiate(pauseMenu);
      
      


    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Game Resumed");
        Destroy(GameObject.Find(pauseMenu.name + "(Clone)"));
     
    }

    void StartGame()
    { }
}

using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] bool isPaused;
    public GameObject player;
   
    void Start()
    {

        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(player);
        instance.transform.position = transform.position;
        instance.transform.rotation = Quaternion.identity;
    }


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

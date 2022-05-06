using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool gameEnded = false;
    private void Start()
    {
        GameManager.Instance.OnGameEnd += GameEnded;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameEnded)
        {
            Time.timeScale = Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    void GameEnded(int starRating)
    {
        gameEnded = true;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

}

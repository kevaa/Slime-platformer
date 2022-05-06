using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject endGameMenu;
    public event Action<int> OnGameEnd = delegate { };
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    float restartTime = .5f;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        player.OnPlayerDeath += RestartLevel;
    }


    public void EndLevel(int starRating)
    {
        Cursor.lockState = CursorLockMode.None;
        endGameMenu.SetActive(true);
        OnGameEnd(starRating);

        StartCoroutine(FadeInEndMenu());
        var key = SceneManager.GetActiveScene().name;
        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetInt(key) < starRating)
            {
                PlayerPrefs.SetInt(key, starRating);
            }
        }
        else
        {
            PlayerPrefs.SetInt(key, starRating);
        }

    }

    public void RestartLevel()
    {
        StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds(restartTime);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator FadeInEndMenu()
    {
        var canvasGroup = endGameMenu.GetComponent<CanvasGroup>();
        var seconds = 0f;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, seconds);
            seconds += Time.deltaTime;
            yield return null;
        }
        Cursor.lockState = CursorLockMode.None;
    }
}

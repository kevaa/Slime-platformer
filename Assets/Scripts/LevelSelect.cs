using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelSelect : MonoBehaviour
{
    public event Action OnReset = delegate { };

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    public void SelectLevel(int i)
    {
        SceneManager.LoadScene("Level" + i);

    }

    public void Reset()
    {
        OnReset();
        PlayerPrefs.DeleteAll();
    }
}

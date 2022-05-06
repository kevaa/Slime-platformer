using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStarRetrieval : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject[] stars;
    [SerializeField] LevelSelect levelSelect;
    private void Start()
    {
        if (levelSelect != null)
        {
            levelSelect.OnReset += OnReset;
        }
        var starCount = 0;
        string key = "Level" + text.text;
        if (PlayerPrefs.HasKey(key))
        {
            starCount = PlayerPrefs.GetInt(key);
        }
        for (int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
        }
    }

    void OnReset()
    {
        foreach (var star in stars)
        {
            star.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] GameObject[] stars;

    private void Start()
    {
        GameManager.Instance.OnGameEnd += OnGameEnd;
    }

    void OnGameEnd(int starRating)
    {
        GetComponent<CanvasGroup>().interactable = true;
        for (int i = 0; i < starRating; i++)
        {
            stars[i].SetActive(true);
        }
    }
}

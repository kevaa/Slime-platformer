using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] int totalCollectibles;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            GameManager.Instance.EndLevel(Mathf.FloorToInt(player.collectiblesFound * 3 / totalCollectibles));
        }
    }
}

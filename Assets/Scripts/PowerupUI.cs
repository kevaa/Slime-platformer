using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerupUI : MonoBehaviour
{
    [SerializeField] Image powerupImage;
    [SerializeField] List<Sprite> powerupSprites;
    [SerializeField] List<string> powerupNames;
    [SerializeField] Player player;
    float timer = 0f;
    float powerupTime = -1f;
    [SerializeField] Image progressBar;

    private void Awake()
    {
        player.OnPowerupCollected += OnPowerupCollected;
    }
    void OnPowerupCollected(PowerupType type, float seconds)
    {
        var index = powerupNames.IndexOf(type.ToString());
        if (index != -1)
        {
            powerupImage.sprite = powerupSprites[index];
            var c = powerupImage.color;
            c.a = 1f;
            powerupImage.color = c;
        }
        powerupTime = seconds;
        timer = 0f;
    }

    private void Update()
    {
        if (powerupTime != -1)
        {
            if (timer < powerupTime)
            {
                timer += Time.deltaTime;
                progressBar.fillAmount = timer / powerupTime;
            }
            else
            {
                powerupTime = -1f;
                timer = 0f;
                var c = powerupImage.color;
                c.a = 0f;
                powerupImage.color = c;
                progressBar.fillAmount = 0;

            }
        }
    }
}

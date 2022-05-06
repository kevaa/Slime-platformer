using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;


public class Player : MonoBehaviour, IDamageable, ICollector, IPowerupPickuper
{
    int lives = 1;
    public int collectiblesFound { get; private set; } = 0;
    public event Action OnPlayerDeath = delegate { };
    public event Action<PowerupType, float> OnPowerupCollected = delegate { };
    public event Action<PowerupType> OnPowerupTimeup = delegate { };

    float powerupTimer = 0f;
    float powerupTimeup = -1;

    PowerupType powerupType;

    bool levelEnded = false;
    public void TakeDamage(int damage)
    {
        lives -= damage;
        if (lives == 0)
        {
            OnPlayerDeath();
        }
    }

    public void Kill()
    {
        lives = 0;
        OnPlayerDeath();
    }

    public void Collect()
    {
        collectiblesFound++;
    }

    public void Pickup(PowerupType type, float seconds)
    {
        OnPowerupCollected(type, seconds);
        powerupTimeup = seconds;
        powerupType = type;
        powerupTimer = 0f;
    }

    private void Update()
    {
        if (powerupTimeup != -1)
        {
            if (powerupTimer >= powerupTimeup)
            {
                OnPowerupTimeup(powerupType);
                powerupTimeup = -1f;
            }
            else
            {
                powerupTimer += Time.deltaTime;
            }
        }

    }

}

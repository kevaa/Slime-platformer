using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    public event Action<Vector2> OnLaunch = delegate { };
    private float pushStrength = 18f;
    const float defaultPushStrength = 18f;
    const float pushStrengthPowerup = 25f;
    [SerializeField] float minForceToMove = .2f;
    private Vector2 prevStretchVect = Vector2.zero;
    public event Action OnCharge = delegate { };
    public event Action<Vector2> OnCharging = delegate { };

    public event Action OnStopCharge = delegate { };

    float chargeThreshold = .01f;
    bool charging = false;
    bool startedPreChargeTimer = false;
    float minChargeTime = .05f;
    bool isMinChargeTimePassed = false;
    float stretchVectMultiplier = .5f;
    float chargeTime = 0f;
    [SerializeField] Player player;

    private void Awake()
    {
        player.OnPowerupCollected += OnPowerupCollected;
        player.OnPowerupTimeup += OnPowerupTimeup;
    }
    public void ExtendTo(Vector2 playerPos, Vector2? stretchVectNull)
    {
        if (charging && stretchVectNull == null)
        {
            transform.localPosition = Vector2.zero;
            if (prevStretchVect.magnitude > minForceToMove)
            {
                var finalForce = -prevStretchVect * pushStrength;
                OnLaunch(finalForce);
                prevStretchVect = Vector2.zero;
            }
            charging = false;
            chargeTime = 0f;

            OnStopCharge();
            isMinChargeTimePassed = false;
            startedPreChargeTimer = false;
        }

        else if (stretchVectNull != null && stretchVectNull.Value.magnitude > chargeThreshold)
        {
            if (chargeTime < minChargeTime)
            {
                chargeTime += Time.deltaTime;
            }
            else
            {
                if (!charging)
                {
                    charging = true;
                    OnCharge();
                }
                var stretchVect = stretchVectNull.Value * stretchVectMultiplier;

                OnCharging(-stretchVect * pushStrength);
                transform.position = playerPos + stretchVect;
                prevStretchVect = stretchVect;
            }
        }
        else
        {
            chargeTime = 0f;
        }
    }

    public void Reset()
    {
        if (charging)
        {
            charging = false;
            chargeTime = 0f;
            OnStopCharge();
            isMinChargeTimePassed = false;
            startedPreChargeTimer = false;
        }
    }

    void OnPowerupCollected(PowerupType type, float seconds)
    {
        switch (type)
        {
            case PowerupType.LongJump:
                {
                    pushStrength = pushStrengthPowerup;
                    break;
                }
        }
    }

    void OnPowerupTimeup(PowerupType type)
    {
        switch (type)
        {
            case PowerupType.LongJump:
                {
                    pushStrength = defaultPushStrength;
                    break;
                }
        }
    }
}




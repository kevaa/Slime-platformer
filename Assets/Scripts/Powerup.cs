using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;


public class Powerup : MonoBehaviour
{
    [SerializeField] PowerupType type;
    [SerializeField] float seconds;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var powerupPickuper = other.GetComponent<IPowerupPickuper>();
        if (powerupPickuper != null)
        {
            powerupPickuper.Pickup(type, seconds);
        }
    }

}

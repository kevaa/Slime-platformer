// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.PlayerLoop;

// /** Manages force charging and charging particle system **/
// public class ForceCharger: MonoBehaviour
// {
//     [SerializeField] private float magnitudePerSec = 1f;
//     [SerializeField] private float maxChargeTime = 3f;
//     private float chargeStartTime;
//     [SerializeField] private ParticleSystem chargeParticles;
//     public bool IsCharging { get; private set; }

//     void Start()
//     {
//         chargeParticles.Stop();
//     }
//     public void StartCharging()
//     {
//         chargeParticles.Play();
//         chargeStartTime = Time.time;
//         IsCharging = true;
//     }

//     public void StopCharging()
//     {
//         chargeParticles.Stop();
//         IsCharging = false;
//     }


//     public float GetChargeMagnitude()
//     {
//         var chargeSec = Time.time - chargeStartTime;
//         chargeSec = Mathf.Clamp(chargeSec, 0, maxChargeTime);

//         var finalMagnitude = 1 +  chargeSec * magnitudePerSec;
//         return finalMagnitude;
//     }


// }

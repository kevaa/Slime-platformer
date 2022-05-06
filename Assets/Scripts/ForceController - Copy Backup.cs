// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ForceController : MonoBehaviour
// {
//     public event Action<Vector2> OnForceReady = delegate { };

//     [SerializeField] private float maxStretchLength = 0.5f;
//     [SerializeField] float minForceToMove = .2f;
//     private float chargeThreshold = 1f;
//     private Vector2 prevPos;
//     private ForceCharger charger;



//     private void Awake()
//     {
//         charger = GetComponent<ForceCharger>();
//     }

//     public void ExtendTo(Vector2? stretchVectNull, bool playChargeParticles = false)
//     {
//         // if not dragging, trigger movement and reset to origin
//         if (stretchVectNull == null)
//         {
//             transform.localPosition = Vector2.zero;
//             if (prevPos.magnitude > minForceToMove)
//             {
//                 var chargedForce = 1f;
//                 if (charger.IsCharging)
//                 {
//                     chargedForce = charger.GetChargeMagnitude();
//                     charger.StopCharging();
//                 }

//                 var finalForce = -prevPos * chargedForce;

//                 OnForceReady(finalForce);
//                 prevPos = Vector2.zero;
//             }
//         }

//         // if dragging, prepare for launch
//         else
//         {
//             Vector2 stretchVect = Vector2.ClampMagnitude(stretchVectNull.Value, maxStretchLength);

//             var localPos = transform.localPosition;
//             // Update position only if magnitude is smaller or changed direction
//             var smallerMagnitude = localPos.magnitude < stretchVect.magnitude - chargeThreshold;
//             var differentDirection = !localPos.normalized.Equals((Vector3)stretchVect.normalized);
//             if (smallerMagnitude || differentDirection)
//             {
//                 transform.localPosition = stretchVect;
//                 prevPos = stretchVect;
//             }

//             var maxDist = transform.localPosition.magnitude > maxStretchLength - 0.1;
//             if (maxDist)
//             {
//                 if (!charger.IsCharging && playChargeParticles)
//                 {
//                     charger.StartCharging();
//                 }
//             }
//             else
//             {
//                 charger.StopCharging();
//             }
//         }
//     }

// }

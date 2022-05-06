using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] ForceController forceController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement.OnFall += OnFall;
        playerMovement.OnGrounded += OnGrounded;
        forceController.OnCharge += OnCharge;
        forceController.OnStopCharge += OnStopCharge;
        playerMovement.OnUpwardsForce += OnLaunch;
        playerMovement.OnRolling += OnRolling;
        playerMovement.OnStopRolling += OnStopRolling;
        playerMovement.OnBreaking += OnBreaking;
        playerMovement.OnStopBreaking += OnStopBreaking;
        playerMovement.GetComponent<Player>().OnPlayerDeath += OnDamaged;
    }

    void OnGrounded()
    {
        animator.SetTrigger("HitGround");
        animator.SetBool("Jump", false);
        animator.SetBool("Fall", false);
    }

    void OnBreaking()
    {
        animator.SetBool("IsBreaking", true);
    }

    void OnStopBreaking()
    {
        animator.SetBool("IsBreaking", false);
    }

    void OnLaunch(Vector2 vect)
    {
        animator.SetBool("Jump", true);
        animator.ResetTrigger("HitGround");
    }
    void OnLaunch()
    {
        animator.SetBool("Jump", true);
        animator.SetBool("Fall", false);

        animator.ResetTrigger("HitGround");
    }

    void OnFall()
    {
        animator.SetBool("Fall", true);
        animator.SetBool("Jump", false);

        animator.ResetTrigger("HitGround");

    }

    void OnCharge()
    {
        animator.SetBool("IsCharging", true);
    }

    void OnStopCharge()
    {
        animator.SetBool("IsCharging", false);
    }

    void OnRolling()
    {
        animator.SetBool("IsRolling", true);
    }

    void OnStopRolling()
    {
        animator.SetBool("IsRolling", false);
    }

    void OnDamaged()
    {
        animator.SetTrigger("Damaged");
    }
}

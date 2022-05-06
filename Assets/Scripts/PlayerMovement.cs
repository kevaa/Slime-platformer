using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput input;
    [SerializeField] private ForceController forceController;

    Rigidbody2D rb;
    [SerializeField] int maxLaunchesInAir = 0;
    [SerializeField] private int midairLaunches = 0;
    [SerializeField] float rollSpeed = 5f;
    bool grounded = false;
    public event Action OnGrounded = delegate { };

    public event Action OnFall = delegate { };
    public event Action OnUpwardsForce = delegate { };
    public event Action OnRolling = delegate { };
    public event Action OnStopRolling = delegate { };
    public event Action OnBreaking = delegate { };
    public event Action OnStopBreaking = delegate { };

    [SerializeField] float breakMultiplier = 1.5f;

    bool falling = false;
    const float fallingThreshold = -1f;
    const float jumpingThreshold = .001f;
    bool facingRight = true;
    bool jumping = false;
    bool rolling = false;
    [SerializeField] float distFromGround;
    bool launching = false;
    const float launchToRollCooldown = 1f;
    bool readyToRoll = false;
    const float readyToRollTimeout = .25f;
    float timeSinceGrounded = 0f;
    float turnThreshold = .5f;
    bool breaking = false;
    bool doubleJumpCoroutineStarted = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        GetComponent<Player>().OnPowerupCollected += OnPowerupCollected;
        GetComponent<Player>().OnPowerupTimeup += OnPowerupTimeup;

        forceController.OnLaunch += Launch;
        forceController.OnCharge += OnCharging;
        forceController.OnStopCharge += OnStopCharge;
    }

    private void Start()
    {
        GameManager.Instance.OnGameEnd += OnEndLevel;
    }
    void Update()
    {
        if (grounded || midairLaunches < maxLaunchesInAir)
        {
            forceController.ExtendTo(transform.position, input.DragVect);
        }
        else
        {
            forceController.Reset();
        }
    }

    private void FixedUpdate()
    {
        if (input.PressingBreakBtn)
        {
            breaking = true;
            OnBreaking();
            rb.velocity *= new Vector2(1 - (breakMultiplier * Time.fixedDeltaTime),
                rb.velocity.y > 0 ? 1 - (breakMultiplier * Time.fixedDeltaTime) : 1f);
        }
        else if (breaking && !input.PressingBreakBtn)
        {
            OnStopBreaking();
        }
        if (!facingRight && rb.velocity.x > turnThreshold || facingRight && rb.velocity.x < -turnThreshold)
        {
            SwitchDirection();
        }
        if (!grounded && IsGrounded())
        {
            OnGrounded();
            midairLaunches = 0;
            grounded = true;
            falling = false;
            jumping = false;
        }
        else if (grounded && !IsGrounded())
        {
            grounded = false;
            readyToRoll = false;
            timeSinceGrounded = 0f;
        }
        if (!falling && !grounded && rb.velocity.y < fallingThreshold)
        {
            OnFall();
            falling = true;
            jumping = false;
        }
        else if (!jumping && !grounded && rb.velocity.y > jumpingThreshold)
        {
            OnUpwardsForce();
            jumping = true;
            falling = false;
        }

        if (grounded && !readyToRoll)
        {
            timeSinceGrounded += Time.fixedDeltaTime;
            if (timeSinceGrounded >= readyToRollTimeout)
            {
                readyToRoll = true;
            }
        }
        if (readyToRoll && !launching && input.Horizontal != 0)
        {
            if (facingRight && input.Horizontal < 0 || !facingRight && input.Horizontal > 0)
            {
                SwitchDirection();
            }
            if (!rolling)
            {
                rolling = true;
                rb.velocity = Vector3.zero;
            }
            OnRolling();
            rb.MovePosition((Vector2)transform.position + (new Vector2(input.Horizontal, 0) * rollSpeed * Time.fixedDeltaTime));
        }
        else if (launching || (rolling && input.Horizontal == 0))
        {
            rolling = false;
            OnStopRolling();
        }

    }

    void SwitchDirection()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }
    void Launch(Vector2 vect)
    {
        rb.velocity = vect;
        if (!grounded)
        {
            midairLaunches++;
        }
    }

    private bool IsGrounded()
    {
        bool leftGrounded = Physics2D.Raycast(transform.position - (Vector3.left * .7f), Vector2.down, distFromGround, LayerMask.GetMask("Ground"));
        bool rightGrounded = Physics2D.Raycast(transform.position - (Vector3.right * .7f), Vector2.down, distFromGround, LayerMask.GetMask("Ground"));
        bool middleGrounded = Physics2D.Raycast(transform.position, Vector2.down, distFromGround, LayerMask.GetMask("Ground"));

        return leftGrounded || rightGrounded || middleGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + (Vector2.down * distFromGround));
    }

    void OnStopCharge()
    {
        StartCoroutine(OnStopChargeCoroutine());
    }

    IEnumerator OnStopChargeCoroutine()
    {
        yield return new WaitForSeconds(launchToRollCooldown);
        launching = false;
    }

    void OnCharging()
    {
        launching = true;
    }

    void OnPowerupCollected(PowerupType type, float seconds)
    {
        switch (type)
        {
            case PowerupType.DoubleJump:
                {
                    maxLaunchesInAir = 1;
                    break;
                }
        }
    }

    void OnPowerupTimeup(PowerupType type)
    {
        switch (type)
        {
            case PowerupType.DoubleJump:
                {
                    maxLaunchesInAir = 0;
                    break;
                }
        }
    }

    void OnEndLevel(int stars)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
    }

}

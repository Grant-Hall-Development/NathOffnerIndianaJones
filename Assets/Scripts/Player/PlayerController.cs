using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    [Header("References")]
    public Rigidbody2D rb2D;
    public PlayerCombat combatController;
    public PlayerAnimation animController;
    public SpriteRenderer playerVisual;

    [Header("Movement Stats")]
    public float movementSpeed;

    [Header("Jump Stats")]
    public float jumpPower;
    public LayerMask floorLayer;
    public Transform floorDetectionTransform;
    public float floorDetectionDistance;

    [Header("Bool Checks")]
    public bool canAttack;
    public bool isGrounded;
    public bool IsGrounded
    {
        get => isGrounded;
        set
        {
            if(isGrounded != value)
            {
                isGrounded = value;
                PlayMovementAnimations(isGrounded);
            }
        }
    }
    public bool isOverrideActive;

    private void Start()
    {
        base.Start();
        IsGrounded = true;
        canAttack = true;
    }

    private void Awake()
    {
        References();
    }

    public void References()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            TakeDamage(1);

        IsGrounded = GetIsGrounded();

        if (isOverrideActive)
            return;


        if (GetIsDying())
            return;

        InputDetection();
        CombatInputDetection();
    }

    public bool CanAttack()
    {
        if (isOverrideActive)
            return false;

        if (!GetCanAttack())
            return false;

        if (!canTakeDamage)
            return false;

        if (!combatController.CanAttackTimer())
            return false;

        return true;
    }

    public void CombatInputDetection()
    {
        combatController.ReduceTimer();

        if (!CanAttack())
            return;

     
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WhipLogic();
        }
    }

    public void WhipLogic()
    {
        if (!CanAttack())
            return;

        combatController.ResetTimer();
        ForceStopMovement();
        canAttack = false;
        combatController.Attack();
    }

    public void InputDetection()
    {
        if (isOverrideActive)
            return;

        if (!canAttack)
            return;

        if (!canTakeDamage)
            return;

        if (MobileChecker.Instance.isMobile) return;

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        float h = Input.GetAxisRaw("Horizontal");
        Movement(h);
    }

    public void MovementLogic(float dir)
    {
        if (isOverrideActive)
            return;

        if (!canAttack)
            return;

        if (!canTakeDamage)
            return;

        Movement(dir);
    }

    public void JumpLogic()
    {
        if (isOverrideActive)
            return;

        if (!canAttack)
            return;

        if (!canTakeDamage)
            return;

        Jump();
    }

    private void Jump()
    {
        if (!GetIsGrounded())
            return;

        animController.PlayJumpAnimation();

        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);
    }

    public void PlayMovementAnimations(bool isGrounded)
    {
        if (isGrounded)
            animController.PlayLocomotionAnimation();
    }

    public void Movement(float hMovement)
    {
        float speed = hMovement * movementSpeed;
        animController.SetPlayerSpeed(Mathf.Abs(speed), movementSpeed);
        PlayerRotation(hMovement);
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
    }

    public void PlayerRotation(float currentSpeed)
    {
        if (currentSpeed < 0)
        {
            damagePivot.localScale = new Vector2(-1, 1);
            playerVisual.flipX = true;
        }
        else if (currentSpeed > 0)
        {
            damagePivot.localScale = new Vector2(1, 1);
            playerVisual.flipX = false;
        }
    }

    public bool GetIsGrounded()
    {
        ContactFilter2D filter = new ContactFilter2D()
        {
            layerMask = floorLayer,
            useLayerMask = true
        };
        return Physics2D.Raycast(floorDetectionTransform.position, Vector2.down, floorDetectionDistance, floorLayer);
    }

    public bool GetCanAttack()
    {
        if (!isGrounded) return false;
        if (!canAttack) return false;
        return true;
    }

    public IEnumerator ForceMovement(float timeForced)
    {
        isOverrideActive = true;

        float t = 0;
        while(t < timeForced)
        {
            t += Time.deltaTime;
            Movement(1);
            yield return null;
        }
        Movement(0);
        isOverrideActive = false;
    }

    public void ForceStopMovement()
    {
        rb2D.velocity = Vector2.zero;
    }

    public void ForceDisplayMovement(int direction)
    {
        animController.SetPlayerSpeed(direction, direction);
        PlayerRotation(direction);
    }

    #region CollisionLogic
    public int WhatDirectionToTransform(Transform from)
    {
        Vector2 direction = from.transform.position - transform.position;
        return (direction.x > 0) ? 1 : -1;
    }
    public void Hitback(Transform hitFrom)
    {
        if (health < -0)
            return;

        FindObjectOfType<CameraShakeController>().Shake(0.6f, 0.3f);

        animController.PlatHitAnimation();
        animController.SetPlayerSpeed((0), movementSpeed);

        canTakeDamage = false;
        canAttack = true;
        Vector2 hitDirection = new Vector2(-WhatDirectionToTransform(hitFrom) * hitBackStrength, hitBackHeight);
        rb2D.velocity = hitDirection;
    }

    #endregion CollisionLogic

    #region Events

    #endregion Events

    #region ToggleBoolChecks

    public void ToggleCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;
    }

    #endregion ToggleBoolChecks

    #region Overrides

    public override void TriggerDeath()
    {
        canTakeDamage = true;
    }

    public override void ChangeHealthVisual(int currentHealth)
    {
        PlayerDisplay playerDisplay = FindObjectOfType<PlayerDisplay>();
        if (playerDisplay) playerDisplay.healthBar.SetNewValue(currentHealth, maxHealth);
    }

    public override void DeathAnimation()
    {
        animController.PlayDeathAnimation();
    }

    #endregion Overrides

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(floorDetectionTransform)Gizmos.DrawRay(floorDetectionTransform.position, Vector2.down * floorDetectionDistance);
    }

    
}

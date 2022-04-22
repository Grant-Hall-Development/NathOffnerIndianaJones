using Base.Audio;
using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    public PlayerController playerController;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        anim.Play("Attack");
        AudioManager.Instance.PlaySFX("Whip");
    }

    public void PlayFinishAttackAnimation()
    {
        playerController.ToggleCanAttack(true);
    }

    public void PlayJumpAnimation()
    {
        anim.Play("Jump");
        AudioManager.Instance.PlaySFX("Jump");
    }

    public void PlayDeathAnimation()
    {
        anim.Play("Death");
    }

    public void PlayLocomotionAnimation()
    {
        anim.Play("Locomotion");
    }

    public void SetPlayerSpeed(float currentSpeed, float maxSpeed)
    {
        float speedPercentage = currentSpeed / maxSpeed;
        anim.SetFloat("Speed", speedPercentage);
    }

    internal void PlatHitAnimation()
    {
        anim.Play("Hit");
    }
}

using Base.Audio;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossAnimator : MonoBehaviour
{
    Animator anim;

    public Boss boss;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void PlayAttackAnimation()
    {
        anim.Play("Attack");
    }

    public void PlayFinishAttackAnimation()
    {
        boss.ToggleCanAttack(true);
    }

    public void PlayDeathAnimation()
    {
        anim.Play("Death");
    }

    public void PlayLocomotionAnimation()
    {
        anim.Play("Locomotion");
    }

    public void SetEnemySpeed(float currentSpeed, float maxSpeed)
    {
        float speedPercentage = currentSpeed / maxSpeed;
        anim.SetFloat("Speed", speedPercentage);
    }

    public void DashAttack()
    {
        boss.TriggerDash();
    }
}
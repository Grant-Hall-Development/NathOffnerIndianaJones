using Base.Audio;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    Animator anim;

    public Soldier solider;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void PlayAttackAnimation()
    {
        anim.Play("Attack");
        AudioManager.Instance.PlaySFX("EnemyAttack");

    }

    public void PlayFinishAttackAnimation()
    {
        solider.ToggleCanAttack(true);
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
}


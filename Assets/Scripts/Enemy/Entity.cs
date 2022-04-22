using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer rend;
    public Transform damagePivot;

    [Header("Health Stats")]
    public bool canDie;
    public int maxHealth;
    public int health;

    [Header("Combat Stats")]
    public float hitInvunerablilityTime;

 

    public bool canTakeDamage;
    public float hitBackStrength;
    public float hitBackHeight;
    
    [Header("Death Stats")]
    public float deathDelayTime;


    public void Start()
    {
        health = maxHealth;
        canTakeDamage = true;
    }

    public void TakeDamage(int damageTaken)
    {
        if (coroutine_HitInvunerability != null)
            return;

        AdjustHealth(-damageTaken);

        if (health <= 0)
        {
            TriggerDeath();
            return;
        }
        else
        {
            OnHit();
            return;
        }
    }


    public void AdjustHealth(int healthAdjustment)
    {
        health += healthAdjustment;
        ChangeHealthVisual(health);
    }

    public void SetHealth(int health)
    {
        this.health = health;
        ChangeHealthVisual(health);
    }

    public virtual void ChangeHealthVisual(int currentHealth)
    {
        
    }

    public void OnHit()
    {
        if (coroutine_HitInvunerability == null)
        {
            coroutine_HitInvunerability = StartCoroutine(IHitInvunerability());
        }
    }

    Coroutine coroutine_HitInvunerability;
    public IEnumerator IHitInvunerability()
    {
        Hitback();
        canTakeDamage = false;

        rend.transform.DOPunchScale(Vector3.one * .2f, hitInvunerablilityTime / 4, elasticity: 0.1f) ;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(rend.DOFade(0, hitInvunerablilityTime / 8)).
            Append(rend.DOFade(1, hitInvunerablilityTime / 8)).
            SetLoops(4);

        yield return sequence.WaitForCompletion();
        
        canTakeDamage = true;
        coroutine_HitInvunerability = null;

    }

 

    public virtual void Hitback()
    {

    }


    public virtual void TriggerDeath()
    {
        if (!canDie)
            return;

        if (coroutine_DelayDeath == null)
        {
            coroutine_DelayDeath = StartCoroutine(IDelayDeath(deathDelayTime));
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }


    Coroutine coroutine_DelayDeath;
    public IEnumerator IDelayDeath(float deathDelay)
    {
        DeathAnimation();
        yield return new WaitForSeconds(deathDelay);
        Death();
    }

    internal bool GetIsDying()
    {
        return coroutine_DelayDeath != null;
    }

    public virtual void DeathAnimation()
    {
        
    }

    public virtual int GetDamage()
    {
        return 1;
    }
}

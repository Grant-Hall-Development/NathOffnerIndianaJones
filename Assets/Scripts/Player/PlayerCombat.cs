using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerAnimation animController;

    public int damage;
    public float fireRate;
    public float timer;

    public void ReduceTimer()
    {
        timer -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = fireRate;
    }

    public bool CanAttackTimer()
    {
        return timer <= 0;
    }

    public void Attack()
    {
        animController.PlayAttackAnimation();
    }

}


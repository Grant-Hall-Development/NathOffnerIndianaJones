using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyCollisionDetector enemy))
        {
            enemy.enemyBase.TakeDamage(playerController.combatController.damage);
        }
    }
}

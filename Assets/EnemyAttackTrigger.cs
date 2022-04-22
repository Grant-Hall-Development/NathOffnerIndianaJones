using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    public Entity entityBase;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCollisionDetector player))
        {
            player.playerController.TakeDamage(entityBase.GetDamage());
            player.playerController.Hitback(transform);
        }
    }
}

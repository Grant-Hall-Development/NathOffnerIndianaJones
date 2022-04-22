using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCollisionDetector player))
        {
            player.playerController.TakeDamage(damage);
            player.playerController.Hitback(transform);
        }
    }
}

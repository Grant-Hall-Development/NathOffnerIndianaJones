using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public int nextLevelIndex;
    public float transitionTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print($"Triggered by {collision.name}");

        //[!!!] CHANGE LATER
        PlayFabCustomEvents.Instance.OnBossRoomEntered();

        if (!collision.CompareTag("Player"))
            return;

        StartCoroutine(FindObjectOfType<PlayerController>().ForceMovement(transitionTime));
        LevelTransition.Instance.TransitionOut(nextLevelIndex, transitionTime);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

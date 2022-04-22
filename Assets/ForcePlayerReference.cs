using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePlayerReference : MonoBehaviour
{
    public PlayerController playerController;

    public void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void MoveLeft()
    {
        playerController.MovementLogic(-1f);
    }

    public void MoveRight()
    {
        playerController.MovementLogic(1f);
    }

    public void StopMovement()
    {
        playerController.MovementLogic(0);
    }

    public void Jump()
    {
        playerController.JumpLogic();
    }

    public void Whip()
    {
        playerController.WhipLogic();
    }
}

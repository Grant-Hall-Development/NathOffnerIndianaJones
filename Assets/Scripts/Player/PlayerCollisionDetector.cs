using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    public PlayerController playerController;

    public PlayerController GetController() => playerController;
}

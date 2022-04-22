using Base.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    void Start()
    {
        AudioManager.Instance.PlayTrack("LevelOneTrack");
    }


}

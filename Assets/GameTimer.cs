using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : Singleton<GameTimer>
{
    public TextMeshProUGUI currentTimeText;
    public float currentTime;
    public bool canCount;

    private void Start()
    {
        
        UpdateTime(currentTime);

    }

    private void Update()
    {
        if (!canCount) return;

        currentTime -= Time.deltaTime;
        UpdateTime(currentTime);
    }

    public void UpdateTime(float currentTime)
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss");
    }

    public void OnPause()
    {
        canCount = false;
    }

    public void OnResume()
    {
        canCount = true;
    }
}

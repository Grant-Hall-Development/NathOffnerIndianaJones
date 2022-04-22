using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefeatListener : MonoBehaviour
{
    public StoryPage storyToShow;
    public void OnEnable()
    {

        Boss.OnBossDefeated += Boss_OnBossDefeated;
    }

    private void Boss_OnBossDefeated()
    {
        storyToShow.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Boss.OnBossDefeated -= Boss_OnBossDefeated;
        
    }
}

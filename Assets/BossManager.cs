using Base.Audio;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject mobileControls;

    public StoryPage bossEntryStory;
    public StoryPage bossDefeatedStory;
    public GameObject finalStory;

    public Boss boss;

    public Transform bossTransform;
    public Transform playerTransform;
    public Transform runToTransform;
    public Transform playerStartTransform;
    public Transform bossStartTransform;
    public Transform bossHandTransform;
    public Transform bossCollectArtifactTransform;
    public SpriteRenderer artifactTransform;
    public void OnEnable()
    {

        Boss.OnBossDefeated += CheckEnding;
    }

    private void OnDisable()
    {
        Boss.OnBossDefeated -= CheckEnding;

    }

    public void Start()
    {
        FindObjectOfType<StartTransitionController>().StartTransition();
        AudioManager.Instance.PlayTrack("BossTrack");
        mobileControls.SetActive(false);
    }

    public void TriggerBossEntry()
    {
        bossEntryStory.gameObject.SetActive(true);
    }

    public void CheckEnding()
    {
        
        mobileControls.SetActive(false);
        if (Convert.ToInt32(PlayerDataGetter.Instance.GetReadDataAtKey(PlayerConstants.BossEndingValue)) == 1)
            TriggerBossLeavingWithoutIdol();
        else
            TriggerBossLeaving();
    }

    public void TriggerBossDefeat()
    {
        bossDefeatedStory.gameObject.SetActive(true);
        bossTransform.position = bossStartTransform.position;
        playerTransform.position = playerStartTransform.position;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.PlayerRotation(1);
        playerController.isOverrideActive = true;
        playerController.ForceStopMovement();
        mobileControls.SetActive(false);
    }

    public void TriggerBossLeaving()
    {
        mobileControls.SetActive(false);
        PlayerController playerController = FindObjectOfType<PlayerController>();

        boss.ForceMovementWithoutMovement(-1);
        bossTransform.DOMove(bossCollectArtifactTransform.position, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            artifactTransform.transform.SetParent(bossHandTransform);
            artifactTransform.transform.position = bossHandTransform.position;
            artifactTransform.sortingLayerName = "Path";
            bossTransform.DOMove(runToTransform.position, 3.5f).SetDelay(1f).SetEase(Ease.Linear).OnUpdate(() => { boss.ForceMovementWithoutMovement(1); });
        });

        playerTransform.DOMove(runToTransform.position + Vector3.left, 5).SetDelay(2f).SetEase(Ease.Linear).OnUpdate(() => { playerController.ForceDisplayMovement(1); })
            .OnComplete(() =>
            {
                finalStory.gameObject.SetActive(true);
            });
    }

    public void TriggerBossLeavingWithoutIdol()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();

        /*boss.ForceMovementWithoutMovement(-1);
        bossTransform.DOMove(bossCollectArtifactTransform.position, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            artifactTransform.transform.SetParent(bossHandTransform);
            artifactTransform.transform.position = bossHandTransform.position;
            artifactTransform.sortingLayerName = "Path";
        });*/
        bossTransform.DOMove(runToTransform.position, 2f).SetDelay(1f).SetEase(Ease.Linear).OnUpdate(() => { boss.ForceMovementWithoutMovement(1); });

        playerTransform.DOMove(runToTransform.position + Vector3.left, 5).SetDelay(2f).SetEase(Ease.Linear).OnUpdate(() => { playerController.ForceDisplayMovement(1); })
            .OnComplete(() =>
            {
                finalStory.gameObject.SetActive(true);
            });
    }
}

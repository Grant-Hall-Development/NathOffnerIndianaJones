using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabCustomEvents : Singleton<PlayFabCustomEvents>
{

    public bool startTimer;

    public float timer;

    private void Start()
    {
        PlayFabSettings.DisableFocusTimeCollection = true;
    }

    private void Update()
    {
        if (!startTimer) return;
        timer += Time.deltaTime;
    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void OnGameStarted()
    {
        PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest()
        {
            EventName = "player_started_game"
        },
        result => Debug.Log("Success"),
        error => Debug.LogError(error.GenerateErrorReport()));
    }

    public void OnBossRoomEntered()
    {
        PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest()
        {
            Body = new Dictionary<string, object>() {
            { "Time Took", Mathf.RoundToInt(timer)},
        },
            EventName = "player_boss_started"
        },
        result => Debug.Log("Success"),
        error => Debug.LogError(error.GenerateErrorReport()));
    }


    public void OnBossDefeated()
    {
        PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest()
        {
            Body = new Dictionary<string, object>() {
            { "Time Took", Mathf.RoundToInt(timer)},
        },
            EventName = "player_defeated_boss"
        },
        result => Debug.Log("Success"),
        error => Debug.LogError(error.GenerateErrorReport()));
    }


    public void OnLastPageOpened()
    {
        PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest()
        {
            Body = new Dictionary<string, object>() {
            { "Time Took", Mathf.RoundToInt(timer)},
        },
            EventName = "player_opened_last_page"
        },
        result => Debug.Log("Success"),
        error => Debug.LogError(error.GenerateErrorReport()));
    }
    public void OnLastButtonPressed()
    {
        PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest()
        {
            Body = new Dictionary<string, object>() {
            { "Time Took", Mathf.RoundToInt(timer)},
        },
            EventName = "player_clicked_last_button"
        },
        result => Debug.Log("Success"),
        error => Debug.LogError(error.GenerateErrorReport()));
    }




}
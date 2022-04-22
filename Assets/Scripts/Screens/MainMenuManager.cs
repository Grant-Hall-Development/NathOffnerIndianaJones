using Base.Audio;
using Base.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public CustomButton startButton;
    public CustomButton settingsButton;
    public RawImage backgroundImage;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI gameNameText;
    


    public void Start()
    {
        AudioManager.Instance.PlayTrack("MainMenuTrack");

        startButton.SetupButton(StartGame);
        settingsButton.SetupButton(SettingsPage);

        backgroundImage.texture = PlayerDataGetter.Instance.GetElementAtKey(PlayerConstants.MainScreenBackground).texture;
        playerNameText.text = PlayerDataGetter.Instance.GetReadDataAtKey(PlayerConstants.MainMenuName);
        string gameText = PlayerDataGetter.Instance.GetReadDataAtKey(PlayerConstants.MainMenuGameName);
        if(gameText != null)
        {
            gameNameText.text = "and the \n  " + gameText;
        }
    }

    public void StartGame()
    {
        PlayFabCustomEvents.Instance.StartTimer();
        PlayFabCustomEvents.Instance.OnGameStarted();
        SceneLoader.Instance.LoadScene(2);

    }


    public void SettingsPage()
    {

    }
}

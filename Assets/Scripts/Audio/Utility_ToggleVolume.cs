using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Audio
{

    public class Utility_ToggleVolume : MonoBehaviour
    {
        AudioManager audioManager;
        public Button button;
        public Image buttonImage;
        public Sprite[] buttonSprites;

        private void OnEnable()
        {
            AudioManager.OnMutedChange += AudioManager_OnMutedChange;
        }

        private void OnDisable()
        {
            AudioManager.OnMutedChange -= AudioManager_OnMutedChange;
        }

        private void Awake()
        {
            audioManager ??= AudioManager.Instance;
            button ??= GetComponent<Button>();
            buttonImage ??= GetComponent<Image>();
            SetupButton();
        }

        public void SetupButton()
        {
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX("Tap");
                audioManager.ToggleMute();
                //[!!!] Audio here
            });

            SetMutedSprite(SaveData.IsMuted);
        }

        private void Start()
        {
            SetMutedSprite(SaveData.IsMuted);
        }

        public void SetMutedSprite(bool isMuted)
        {
            int value = Convert.ToInt32(isMuted);
            buttonImage.sprite = buttonSprites[value];
        }

        private void AudioManager_OnMutedChange(bool isMuted)
        {
            SetMutedSprite(isMuted);
        }
    }

}
public static class SaveData
{
    public static int Score;

    public static bool IsMuted
    {
        get => ES3.FileExists("SaveData") ? ES3.KeyExists("IsMuted", "SaveData") ? (bool)ES3.Load("IsMuted", "SaveData") : false : false;
        set
        {
            ES3.Save("IsMuted", value, "SaveData");
        }
    }

    public static void SaveScore(int score)
    {
        ES3.Save("Score", 5);
    }

    public static int LoadScore()
    {
        return (int)ES3.Load("Score");
    }


}



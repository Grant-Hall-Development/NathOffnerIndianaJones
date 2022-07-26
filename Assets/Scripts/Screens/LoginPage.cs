using Base.Managers;
using Base.UI;
using Base.Utility;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginPage : MonoBehaviour
{
    public TMP_InputField nameInput;
    public CustomButton forceLoginButton;
    public CustomButton loginButton;
    public CustomButton registerButton;

    [Header("LoginRequestInfo")]
    public GetPlayerCombinedInfoRequestParams loginParams;

    public void OnEnable()
    {
        SetupButtons();    
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                registerButton.gameObject.SetActive(true);
            }
        }
    }

    public void SetupButtons()
    {
        forceLoginButton.SetupButton(ForceLoginRequest);
        loginButton.SetupButton(LoginRequest);
        registerButton.SetupButton(RegisterRequest);
    }

    public void LoginRequest()
    {
        ApplicationManager.ToggleApplicationProcessing(true);

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            CustomId = nameInput.text,
            InfoRequestParameters = loginParams
        }, (result) =>
        {
            ActiveLoginDetails.SetEntityDetails(result);
            ApplicationManager.TriggerLoginSuccessful(result);
            Pr.Log("Logged in");
        }, (error) =>
        {
            ApplicationManager.ToggleApplicationProcessing(false);
            Pr.Error(error.ErrorMessage);
        });
    }
    public void ForceLoginRequest()
    {
        ApplicationManager.ToggleApplicationProcessing(true);

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            CustomId = "Hugo First",
            InfoRequestParameters = loginParams
        }, (result) =>
        {
            ActiveLoginDetails.SetEntityDetails(result);
            ApplicationManager.TriggerLoginSuccessful(result);
            Pr.Log("Logged in");
        }, (error) =>
        {
            ApplicationManager.ToggleApplicationProcessing(false);
            Pr.Error(error.ErrorMessage);
        });
    }

    public void RegisterRequest()
    {
        ApplicationManager.ToggleApplicationProcessing(true);

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            CreateAccount = true,
            CustomId = nameInput.text
        }, (result) =>
        {
            SetDisplayName();
            Pr.Log("Account created");
        }, (error) =>
        {
            ApplicationManager.ToggleApplicationProcessing(false);
            Pr.Error(error.ErrorMessage);
        });
    }

    public void SetDisplayName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest()
        {
            DisplayName = nameInput.text
        }, (result) =>
        {
            
            Pr.Log($"Updated Display Name to {nameInput.text}");
        }, (error) =>
        {
         
            Pr.Error(error.ErrorMessage);
        });
    }  
}

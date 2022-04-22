using PlayFab.ClientModels;
using System;
using System.Collections;
using UnityEngine;

namespace Base.Managers
{
    public class ApplicationManager : Singleton<ApplicationManager>
    {
        public static event Action<bool> OnApplicationProcessing;
        public static event Action<LoginResult> OnLoginSuccessful;
        public static LoginResult lastLoginResult;
        public static void ToggleApplicationProcessing(bool isProcessing)
        {
            OnApplicationProcessing?.Invoke(isProcessing);
        }

        public static void TriggerLoginSuccessful(LoginResult loginResult)
        {
            ApplicationManager.lastLoginResult = loginResult; 
            OnLoginSuccessful?.Invoke(loginResult);
        }
    }
}
using Base.Managers;
using Base.Utility;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.DataModels;
using PlayFab.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataGetter : SerliazedSingleton<PlayerDataGetter>
{ 
    public List<KeyTexturePairs> keyTexturePairs = new List<KeyTexturePairs>();
    [Space]
    public Dictionary<string, string> allReadData = new Dictionary<string, string>();


    public void OnEnable()
    {
        ApplicationManager.OnLoginSuccessful += ApplicationManager_OnLoginSuccessful;
    }

    private void OnDisable()
    {
        ApplicationManager.OnLoginSuccessful -= ApplicationManager_OnLoginSuccessful;
    }

    private void ApplicationManager_OnLoginSuccessful(LoginResult result)
    {

        GetPlayerReadData(result);
        GetPlayerFilesRequest();
    }

    public void GetPlayerReadData(LoginResult result)
    {
        allReadData.Clear();
        foreach (var data in result.InfoResultPayload.UserReadOnlyData)
        {
            allReadData.Add(data.Key, data.Value.Value);
        }
    }

    public void GetPlayerFilesRequest()
    {
        PlayFabDataAPI.GetFiles(new GetFilesRequest()
        {
            Entity = new PlayFab.DataModels.EntityKey
            {
                Id = ActiveLoginDetails.entityID,
                Type = ActiveLoginDetails.entityType
            }
        }
        ,OnFilesRetrieved, OnFilesFailed);
    }

    private void OnFilesRetrieved(GetFilesResponse obj)
    {
        if(coroutine_OnFilesRetrieved == null)
            coroutine_OnFilesRetrieved = StartCoroutine(IOnFilesRetrieved(obj));
        /*foreach (var filePair in obj.Metadata)
        {
            string toCheck = filePair.Key.Split('.')[0];
            if (!keyTexturePairs.Any(v => v.keyName == toCheck))
            {
                Pr.Error($"File imported {filePair.Key} doesn't match key requests");
                continue;
            }
            KeyTexturePairs pair = keyTexturePairs.FirstOrDefault(k => k.keyName == toCheck);
            SetTexture2DFromMetaData(filePair.Value, pair);
        }*/
    }

    Coroutine coroutine_OnFilesRetrieved;
    public IEnumerator IOnFilesRetrieved(GetFilesResponse obj)
    {
        foreach (var filePair in obj.Metadata)
        {
            string toCheck = filePair.Key.Split('.')[0];
            if (!keyTexturePairs.Any(v => v.keyName == toCheck))
            {
                Pr.Error($"File imported {filePair.Key} doesn't match key requests");
                continue;
            }
            KeyTexturePairs pair = keyTexturePairs.FirstOrDefault(k => k.keyName == toCheck);
            SetTexture2DFromMetaData(filePair.Value, pair);
        }

        yield return new WaitWhile(() => keyTexturePairs.Any(p => p.texture == null));

        ApplicationManager.ToggleApplicationProcessing(false);
        Pr.Log("All Files Found");
        SceneLoader.Instance.LoadScene(1);
    }

    private void OnFilesFailed(PlayFabError obj)
    {
        Pr.Error(obj.ErrorMessage);
    }

    public void SetTexture2DFromMetaData(GetFileMetadata metaData, KeyTexturePairs pair)
    {
        PlayFabHttp.SimpleGetCall(metaData.DownloadUrl,
            result => { pair.SetTexture(GetTexture2D(result, pair.desiredDimensions)); },
            error => { Pr.Error(error); });
    }
    public Texture2D GetTexture2D(byte[] bytes, Vector2 dimensions)
    {
        Texture2D tex = new Texture2D((int)dimensions.x, (int)dimensions.y);
        tex.LoadImage(bytes);
        return tex;
    }
    public string GetReadDataAtKey(string key)
    {
        return allReadData.FirstOrDefault(r => r.Key == key).Value;
    }
    public KeyTexturePairs GetElementAtKey(string key)
    {
        return keyTexturePairs.FirstOrDefault(k => k.keyName == key);
    }
}

[Serializable]
public class KeyTexturePairs
{
    public string keyName;
    public Texture2D texture;
    public Vector2 desiredDimensions;

    public void SetTexture(Texture2D tex)
    {
        texture = tex;
    }
}
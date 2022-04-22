using Base.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class URLButton : MonoBehaviour
{
    CustomButton button;
    public string key;
    private void Start()
    {
        button = GetComponent<CustomButton>();

        button.SetupButton(() =>
        {
            Application.OpenURL(PlayerDataGetter.Instance.GetReadDataAtKey(key));
        });
    }
   
}

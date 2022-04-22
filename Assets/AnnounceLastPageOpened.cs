using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnounceLastPageOpened : MonoBehaviour
{
    public void PlayfabLastPageOpened()
    {
        PlayFabCustomEvents.Instance.OnLastPageOpened();
    }

    public void PlayfabLastButtonClicked()
    {
        PlayFabCustomEvents.Instance.OnLastButtonPressed();
    }
}

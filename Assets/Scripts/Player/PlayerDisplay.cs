using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using UIHealthAlchemy;

public class PlayerDisplay : MonoBehaviour
{
    public RawImage playerIcon;
    public TextMeshProUGUI playerName;
    public HealthBarLogic healthBar;

    public void Start()
    {
        if(PlayerDataGetter.Instance != null)
            SetupPlayerValues();
    }

    public void SetupPlayerValues()
    {
        playerIcon.texture = PlayerDataGetter.Instance.GetElementAtKey(PlayerConstants.PlayerIcon).texture;
        playerName.text = PlayerDataGetter.Instance.allReadData[PlayerConstants.PlayerName];
    }

    
}


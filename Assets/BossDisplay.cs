using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIHealthAlchemy;
using UnityEngine;
using UnityEngine.UI;

public class BossDisplay : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public HealthBarLogic healthBar;

    public void Start()
    {
        if (PlayerDataGetter.Instance != null)
            SetupPlayerValues();
    }

    public void SetupPlayerValues()
    {
        playerName.text = PlayerDataGetter.Instance.allReadData[PlayerConstants.BossName];
    }

}

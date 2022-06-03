using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MobileTextChange : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.text = MobileChecker.Instance.isMobile ? "Tap" : "Next" ;
    }
}

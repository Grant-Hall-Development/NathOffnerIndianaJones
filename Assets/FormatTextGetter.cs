using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FormatTextGetter : MonoBehaviour
{
    public UnityEvent OnTextFinish;

    public TextMeshProUGUI text;
    public string key;
    public string formatInfoKey;
    public float textSpeedInterval;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Start()
    {
        text.text = "";
        if (PlayerDataGetter.Instance == null)
            return;

        string _text = PlayerDataGetter.Instance.GetReadDataAtKey(key);
        text.DOText(_text, _text.Length * textSpeedInterval).OnComplete(() => OnTextFinish?.Invoke());
    }
}

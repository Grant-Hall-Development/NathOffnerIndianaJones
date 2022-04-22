using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDoFadeElements : MonoBehaviour
{
    public Image[] imageElements;
    public TextMeshProUGUI[] textElements;

    private void Start()
    {
        gameObject.SetActive(!MobileChecker.Instance.isMobile);

        TriggerImageFadeTransition(0, 0);
        TriggerTextFadeTransition(0, 0);
    }

    public void TriggerFadeIn()
    {
        TriggerImageFadeTransition(1, 1);
        TriggerTextFadeTransition(1, 1);
    }

    public void TriggerImageFadeTransition(float alpha, float time)
    {
        if (imageElements.Length == 0) return;
        imageElements.ToList().ForEach(i => i.DOFade(alpha, time));
    }
    public void TriggerTextFadeTransition(float alpha, float time)
    {
        if (textElements.Length == 0) return;
        textElements.ToList().ForEach(t => t.DOFade(alpha, time));
    }
}

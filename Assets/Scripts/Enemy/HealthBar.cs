using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float transitionTime;

    public void SetNewHealth(int currentHP, int maxHP)
    {
        float currentPercentage = (float)currentHP / (float)maxHP;
        DOTween.To(() => healthBar.fillAmount, x => healthBar.fillAmount = x, currentPercentage, transitionTime);
    }
}
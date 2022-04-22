using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace UIHealthAlchemy
{
    public abstract class HealthBarLogic : MonoBehaviour
    {
        public float transitionTime;
        public virtual float Value { get; set; }

        public void SetNewValue(int currentHP, int maxHP)
        {
            float currentPercentage = (float)currentHP / (float)maxHP;
            DOTween.To(() => Value, x => Value = x, currentPercentage, transitionTime);
        }

    }
}

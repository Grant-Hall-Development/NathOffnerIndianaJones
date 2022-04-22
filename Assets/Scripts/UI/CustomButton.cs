using Base.Audio;
using Base.Managers;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ContextMenu = UnityEngine.ContextMenu;

namespace Base.UI
{
    public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        public Action OnClick;

        public bool pausesOnProcessing;

        [Header("References")]
        public Image image;
        public Button button;
        public TextMeshProUGUI text;

        [Header("Attributes")]
        [Tooltip("0 is normal, 1 is highlighted")]
        public Sprite[] buttonSprites;
        [Tooltip("0 is normal, 1 is highlighted")]
        public Color[] textColors;

        private Vector3 startScale;

        public bool usesKeyAlternative;
        [ShowIf("usesKeyAlternative")]
        public KeyCode keyPressTrigger;

        private void OnEnable()
        {
            ApplicationManager.OnApplicationProcessing += ToggleUsability;
            EnableValues();


        }

        private void OnDisable()
        {
            ApplicationManager.OnApplicationProcessing -= ToggleUsability;
            DisableValues();
       
        }

        private void Update()
        {
            if (!usesKeyAlternative)
                return;

            if (Input.GetKeyDown(keyPressTrigger))
                OnClick.Invoke();
        }

        

        public void EnableValues()
        {
            if (startScale == Vector3.zero)
                startScale = transform.localScale;

            if (image != null) image.sprite = buttonSprites[0];
            if (text != null) text.color = textColors[0];

            transform.DOScale(startScale, 0.1f);
        }

        public void DisableValues()
        {
            if (image != null) image.sprite = buttonSprites[0];
            if (text != null) text.color = textColors[0];

            transform.DOScale(startScale, 0f);
        }

        public void ToggleUsability(bool isApplicationThinking)
        {
            if(!pausesOnProcessing)
            {
                ToggleInteractable(true);
                return;
            }

            ToggleInteractable(!isApplicationThinking);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (image != null) image.sprite = buttonSprites[1];
            if (text != null) text.color = textColors[1];

            transform.DOScale(startScale * 1.1f, 0.1f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(image != null) image.sprite = buttonSprites[0];
            if(text != null) text.color = textColors[0];

            transform.DOScale(startScale, 0.1f);
        }

        

        public void SetupButton(Action action)
        {
            
            print($"{name}");
            OnClick = action;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { action?.Invoke(); });
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(startScale, 0.1f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {

            transform.DOScale(startScale * 0.9f, 0.1f);
            if (image != null) image.sprite = buttonSprites[1];
            if (text != null) text.color = textColors[1];

        }

        public void ToggleInteractable(bool isActive)
        {
            button.interactable = isActive;
        }

        [ContextMenu("Get Button Reference")]
        public void FindButtonReference()
        {
            button = GetComponent<Button>();
        }
        [ContextMenu("Get Image Reference")]
        public void FindImageReference()
        {
            image = GetComponent<Image>();
        }
        [ContextMenu("Get Both References")]
        public void FindBothReferences()
        {
            FindButtonReference(); 
            FindImageReference();
        }

        [ContextMenu("Get Text Reference")]
        public void GetTextComponent()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}

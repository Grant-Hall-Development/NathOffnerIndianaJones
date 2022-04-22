using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public UnityEvent OnButtonDown;
    public UnityEvent OnButtonUp;

    public Sprite[] buttonStateSprites;
    public Image buttonImage;

    public bool canHold;

    bool isHoldingDown;
    

    void Start()
    {
        buttonImage.sprite = buttonStateSprites[0];
        buttonImage.DOFade(1, 0.1f);

    }

    public void Update()
    {
        print("This is a button");
        if (!canHold) return;

        if (!isHoldingDown) return;
        print("Held down");
        OnButtonDown?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = buttonStateSprites[1];

        OnButtonDown?.Invoke();
        buttonImage.DOFade(0.8f,0.1f);
        print("This is down now");
        isHoldingDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonUp?.Invoke();
        buttonImage.sprite = buttonStateSprites[0];
        buttonImage.DOFade(1,0.1f);
        isHoldingDown = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnButtonUp?.Invoke();
        buttonImage.sprite = buttonStateSprites[0];
        buttonImage.DOFade(1, 0.1f);

        isHoldingDown = false;
    }
}

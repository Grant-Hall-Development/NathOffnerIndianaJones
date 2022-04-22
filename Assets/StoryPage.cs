using Base.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryPage : MonoBehaviour
{
    public UnityEvent OnStoryOpened;
    public UnityEvent OnStoryFinish;

    public CustomButton nextButton;
    public bool triggerTransitionIn;
    public void Start()
    {
        OnStoryOpened?.Invoke();

        nextButton.gameObject.SetActive(false);
        nextButton.SetupButton(() => { if(triggerTransitionIn)LevelTransition.Instance.TriggerTransitionIn(); OnStoryFinish?.Invoke(); });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartTransitionController : MonoBehaviour
{
    public UnityEvent OnTransitionInFinishAction;

    public StoryPage storyPage;

    public float transitionTime;

    public GameObject startWall;
    public Transform startPosition;

    public bool ifTriggerStartTransition;

    private void OnEnable()
    {
        LevelTransition.OnTransitionInFinish += LevelTransition_OnTransitionInFinish;
        LevelTransition.OnTransitionInTrigger += StartTransition;
    }
    private void OnDisable()
    {
        LevelTransition.OnTransitionInFinish -= LevelTransition_OnTransitionInFinish;
        LevelTransition.OnTransitionInTrigger -= StartTransition;
    }

    private void Start()
    {
        
        if (ifTriggerStartTransition) StartTransition();
    }

    public void LevelTransition_OnTransitionInFinish()
    {
        OnTransitionInFinishAction?.Invoke();
        startWall.SetActive(true);
    }


    public void StartTransition()
    {
        startWall.SetActive(false);
        PlayerController controller = FindObjectOfType<PlayerController>();
        controller.transform.position = startPosition.position;
        StartCoroutine(controller.ForceMovement(transitionTime));

        LevelTransition.Instance.TransitionIn(transitionTime);
    }
}

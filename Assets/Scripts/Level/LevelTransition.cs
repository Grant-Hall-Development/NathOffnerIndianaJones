using DG.Tweening;
using System;
using UnityEngine.UI;

public class LevelTransition : Singleton<LevelTransition>
{
    public static event Action OnTransitionInTrigger;
    public static event Action OnTransitionInFinish;

    public Image transitionImage;

    public void TransitionOut(int levelIndexToGoTo, float time)
    {
        transitionImage.DOFade(0, 0);
        transitionImage.DOFade(1, time).OnComplete(() => SceneLoader.Instance.LoadScene(levelIndexToGoTo));
    }

    public void TransitionIn(float time)
    {
        transitionImage.DOFade(1, 0);
        transitionImage.DOFade(0, time).OnComplete(() => OnTransitionInFinish?.Invoke());

    }

    public void TriggerTransitionIn()
    {
        OnTransitionInTrigger?.Invoke();

}
}

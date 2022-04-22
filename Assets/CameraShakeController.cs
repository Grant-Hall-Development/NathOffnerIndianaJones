using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    CinemachineVirtualCamera cam;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float force, float time)
    {
        CinemachineBasicMultiChannelPerlin shakePerlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Sequence shakeSequence = DOTween.Sequence();
        shakeSequence.Append(DOTween.To(() => shakePerlin.m_AmplitudeGain, x => shakePerlin.m_AmplitudeGain = x, force, 0));
        shakeSequence.Append(DOTween.To(() => shakePerlin.m_AmplitudeGain, x => shakePerlin.m_AmplitudeGain = x, force, time));
        shakeSequence.Append(DOTween.To(() => shakePerlin.m_AmplitudeGain, x => shakePerlin.m_AmplitudeGain = x, 0, 0));
    }
}


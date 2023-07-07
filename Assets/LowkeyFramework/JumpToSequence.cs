using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToSequence : MonoBehaviour
{
    [SerializeField]
    private Transform transformToTween;
    [SerializeField]
    private JumpTween jumpTweenBehaviour;
    [SerializeField]
    private PunchScaleTween punchScaleTweenBehaviour;
    [SerializeField]
    private ScaleTween scaleInTweenBehaviour;
    [SerializeField]
    private ScaleTween scaleOutTweenBehaviour;

    public void JumpToWithPunchScale(Vector3 target, Action onComplete = null, CollidersHandler collidersToTurnOff = null)
    {
        JumpToWithoutScale(target, onComplete, collidersToTurnOff);
        punchScaleTweenBehaviour.Play();
    }

    public void JumpToWithInOutScale(Vector3 target, Action onComplete = null, CollidersHandler collidersToTurnOff = null)
    {
        JumpToWithoutScale(target, onComplete, collidersToTurnOff);
        scaleInTweenBehaviour.Play(() => scaleOutTweenBehaviour.Play());
    }

    public void JumpToWithoutScale(Vector3 target, Action onComplete = null, CollidersHandler collidersToTurnOff = null)
    {
        if (collidersToTurnOff)
        {
            collidersToTurnOff.CollidersSetEnabled(false);
        }
        jumpTweenBehaviour.PlaySequence(target, onComplete);
    }

    private void OnValidate()
    {
        if(transformToTween)
        {
            if(!jumpTweenBehaviour.transformToTween)
            {
                jumpTweenBehaviour.transformToTween = transformToTween;
            }

            if(!punchScaleTweenBehaviour.transformToTween)
            {
                punchScaleTweenBehaviour.transformToTween = transformToTween;
            }

            if(!scaleInTweenBehaviour.transformToTween)
            {
                scaleInTweenBehaviour.transformToTween = transformToTween;
            }

            if(!scaleOutTweenBehaviour.transformToTween)
            {
                scaleOutTweenBehaviour.transformToTween = transformToTween;
            }
        }
    }
}

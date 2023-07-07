using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTween : TweenBehaviour
{
    [Header("Jump tween properties")]
    [SerializeField]
    private Transform targetToJumpTo;
    [SerializeField]
    private float jumpPower = 3f;
    [SerializeField]
    private int jumpsNum = 1;

    public override Tween Play(Action onComplete = null, Transform transformToTween = null) => PlaySequence(targetToJumpTo.position, onComplete, transformToTween);

    public Sequence PlaySequence(Action onComplete = null, Transform transformToTween = null) => PlaySequence(targetToJumpTo.position, onComplete, transformToTween);

    public Sequence PlaySequence(Vector3 target, Action onComplete = null, Transform transformToTween = null) =>
        GetTransformToTween(transformToTween).DOJump(target, jumpPower, jumpsNum, duration)
            .SetEase(ease)
            .OnComplete(() => onComplete?.Invoke());
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : TweenBehaviour
{
    [Header("Scale tween properties")]
    [SerializeField]
    private float scaleEndValue = 2f;

    public override Tween Play(Action onComplete = null, Transform transformToTween = null) =>
        GetTransformToTween(transformToTween).DOScale(scaleEndValue, duration)
            .SetEase(ease)
            .OnComplete(() => onComplete?.Invoke());
}

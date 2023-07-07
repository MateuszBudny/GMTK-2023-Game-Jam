using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScaleTween : TweenBehaviour
{
    [Header("PunchScale tween properties")]
    [SerializeField]
    private float scalePunch = 2f;
    [SerializeField]
    private int vibrato = 10;
    [SerializeField]
    private float elasticity = 1f;

    public override Tween Play(Action onComplete = null, Transform transformToTween = null) =>
        GetTransformToTween(transformToTween).DOPunchScale(new Vector3(scalePunch, scalePunch, scalePunch), duration, vibrato, elasticity)
            .SetEase(ease)
            .OnComplete(() => onComplete?.Invoke());
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TweenBehaviour : MonoBehaviour
{
    [Header("Generic tween properties")]
    public Transform transformToTween;
    [SerializeField]
    protected float duration = 1f;
    [SerializeField]
    protected Ease ease = Ease.InOutQuad;

    public abstract Tween Play(Action onComplete = null, Transform transformToTween = null);

    protected Transform GetTransformToTween(Transform localTransform) => localTransform ?? transformToTween;
}

using System;
using UnityEngine;

[Serializable]
public class ValueProgressedByOffset
{
    [SerializeField]
    private float startingValue = 0f;
    [SerializeField]
    private AnimationCurve offsetProgression;

    public float StartingValue { get => startingValue; set => startingValue = value; }

    public float GetValue(float progress)
    {
        float currentOffset = offsetProgression.Evaluate(progress);
        return startingValue + currentOffset;
    }
}
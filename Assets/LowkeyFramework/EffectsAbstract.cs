using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectsAbstract<TEnum> : MonoBehaviour where TEnum : Enum
{
    public abstract void Play(TEnum effect);

    public virtual void Stop(TEnum effect, ParticleSystemStopBehavior stopBehavior) {}

    public virtual void Stop(TEnum effect) {}

    public virtual void PlayForLimitedTime(TEnum effect, float duration, ParticleSystemStopBehavior stopBehaviour)
    {
        StartCoroutine(PlayForLimitedTimeEnumerator(effect, duration, stopBehaviour));
    }

    protected IEnumerator PlayForLimitedTimeEnumerator(TEnum effect, float duration, ParticleSystemStopBehavior stopBehaviour)
    {
        Play(effect);
        yield return new WaitForSecondsRealtime(duration);

        Stop(effect, stopBehaviour);
    }
}
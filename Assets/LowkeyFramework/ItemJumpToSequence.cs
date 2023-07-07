using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJumpToSequence : JumpToSequence
{
    [SerializeField]
    private CollidersHandler collidersHandler;

    public void JumpTo(Vector3 target, bool scaleTo0, Action onComplete = null, bool turnOffColliders = false)
    {
        CollidersHandler collidersHandler = turnOffColliders ? this.collidersHandler : null;
        if (scaleTo0)
        {
            JumpToWithInOutScale(target, onComplete, collidersHandler);
        }
        else
        {
            JumpToWithPunchScale(target, onComplete, collidersHandler);
        }
    }
}

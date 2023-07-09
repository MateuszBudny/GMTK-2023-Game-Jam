using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveShooterTracking : ShooterTracking
{
    public override Vector3 GetShooterLookAtPosition()
    {
        return TrackedBall.transform.position;
    }
}

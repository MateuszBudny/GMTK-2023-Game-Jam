using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveShooterTracking : ShooterTracking
{
    public override Vector3 GetShooterLookAtPosition(float shootingBallSpeed)
    {
        return TrackedBall.transform.position;
    }
}

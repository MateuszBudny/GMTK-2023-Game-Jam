using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveShooterTracking : ShooterTracking
{
    public override Vector3 GetShooterLookAtPosition(float shootingBallSpeed, Vector3 shootingBallSpawnPosition)
    {
        return TrackedBall.transform.position;
    }
}

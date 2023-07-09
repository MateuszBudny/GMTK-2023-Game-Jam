using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterButSimpleShooterTracking : ShooterTracking
{
    public override Vector3 GetShooterLookAtPosition()
    {
        float distanceToBall = (transform.position - TrackedBall.transform.position).magnitude;
        return TrackedBall.transform.position + TrackedBall.Velocity * distanceToBall / TrackedBall.Velocity.magnitude;
    }
}

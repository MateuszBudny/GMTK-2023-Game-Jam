using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterButSimpleShooterTracking : ShooterTracking
{
    public override Vector3 GetShooterLookAtPosition(float shootingBallSpeed)
    {
        float distanceThatTheShootingBallWillTravelBeforeGettingYou = (transform.position - TrackedBall.transform.position).magnitude;
        float timeThatWillTakeShootingBallToGetYou = distanceThatTheShootingBallWillTravelBeforeGettingYou / shootingBallSpeed;

        return TrackedBall.transform.position + TrackedBall.Velocity * BallManager.Instance.Spline.CalculateLength() * timeThatWillTakeShootingBallToGetYou;
    }
}

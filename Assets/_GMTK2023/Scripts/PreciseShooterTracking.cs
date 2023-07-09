using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciseShooterTracking : ShooterTracking
{
    public override Vector3 GetShooterLookAtPosition(float shootingBallSpeed, Vector3 shootingBallSpawnPosition)
    {
        float distanceThatTheShootingBallWillTravelBeforeGettingYou = (transform.position - TrackedBall.transform.position).magnitude;
        float timeThatWillTakeShootingBallToGetYou = distanceThatTheShootingBallWillTravelBeforeGettingYou / shootingBallSpeed;
        float trackedBallDistanceDuringShootingBallTravel = TrackedBall.Velocity.magnitude * timeThatWillTakeShootingBallToGetYou;

        return BallManager.Instance.Spline.EvaluatePosition(TrackedBall.Progress + trackedBallDistanceDuringShootingBallTravel);
    }
}

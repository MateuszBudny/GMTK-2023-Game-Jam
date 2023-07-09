using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShooterTracking : MonoBehaviour
{
    public Ball TrackedBall { get; set; }

    public abstract Vector3 GetShooterLookAtPosition(float shootingBallSpeed);
}

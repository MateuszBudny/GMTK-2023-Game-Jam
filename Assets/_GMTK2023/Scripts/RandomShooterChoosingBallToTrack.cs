using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShooterChoosingBallToTrack : ShooterChoosingBallToTrack
{
    public override Ball GetBallToTrack()
    {
        return BallManager.Instance.GetRandomBall();
    }
}

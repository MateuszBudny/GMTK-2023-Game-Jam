using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallState State { get => state; set => state = value; }
    public Vector3 Velocity { get; set; }
    
    private BallState state = BallState.InSnake;

    private void Update()
    {
        if(State == BallState.Flying)
        {
            transform.position += Velocity * Time.deltaTime;
        }
    }
}

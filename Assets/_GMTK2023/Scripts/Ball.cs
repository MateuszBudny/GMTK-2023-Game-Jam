using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallState State { get => state; set => state = value; }

    public Vector3 Velocity { get; set; }

    public Color Color { get => meshRenderer.material.color; set => meshRenderer.material.color = value; }
    public float Progress { get => progress; set => progress = value; }

    private BallState state = BallState.InSnake;
    private MeshRenderer meshRenderer;
    private float progress;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>(); 
    }

    private void Update()
    {
        if(State == BallState.Flying)
        {
            transform.position += Velocity * Time.deltaTime;
        }
    }

    public void UpdateParameters(Vector3 newPos, Vector3 dir, float speed)
    {
        transform.position = newPos;
        Velocity = dir.normalized * speed;
    }

    public float distanceToBall(Ball ball)
    {
        return distanceToPoint(ball.transform.position);
    }

    public float distanceToPoint(Vector3 pos)
    {
        return Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(pos.x, pos.z));
    }

}

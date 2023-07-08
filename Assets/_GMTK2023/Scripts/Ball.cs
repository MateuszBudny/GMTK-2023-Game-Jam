using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallState State { get => state; set => state = value; }

    public Vector3 LastPosition { get => lastPosition; set => lastPosition = value; }
    public Vector3 Velocity { get => velocity; set => velocity = value; }

    public Color Color { get => meshRenderer.material.color; set => meshRenderer.material.color = value; }

    private BallState state = BallState.InSnake;
    private Vector3 lastPosition;
    private Vector3 velocity;
    private MeshRenderer meshRenderer;
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

    public void UpdateParameters(Vector3 pos, Vector3 dir, float speed)
    {
        this.transform.position = pos;
        velocity = dir.normalized * speed;
    }
}

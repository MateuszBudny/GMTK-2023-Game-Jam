﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Splines;

public class BallManager : MonoBehaviour
{

    [SerializeField]
    SplineContainer spline;
    [SerializeField]
    List<Color> colors = new List<Color>();

    [SerializeField]
    int numOfBalls = 10;
    [SerializeField]
    float ballRadius = 1;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float slowDown = 5;
    [SerializeField]
    float speedUp = 5;

    float actualSpeed = 5;

    [SerializeField]
    Ball ballTemplate;

    Vector3 spawnPoint = Vector3.zero;


    int selection = 0;
    GameObject head;
    public List<Ball> balls = new List<Ball>();
    List<float> ballOffsets = new List<float>();


    Ball trackedBall;
    float expectedOffset;
    Vector3 expectedPosition;
    int collisionSpot = -1;

    // Start is called before the first frame update
    void Start()
    {
        selection = 1;

        spawnPoint = spline.EvaluatePosition(0);
    }

    // Update is called once per frame
    void Update()
    {
        actualSpeed = speed;

        HandleInput();

        if(collisionSpot > 0)
        {
            InsertBall();
        }
        else
        {
            MoveBalls();
        }


        if(trackedBall != null && collisionSpot < 0)
        {
            collisionSpot = CheckCollision();
            if(collisionSpot > 0)
            {
                expectedOffset = ExpectedOffset(collisionSpot);
                expectedPosition = spline.EvaluatePosition(expectedOffset);
            }
        }

        HighlightSelectedBalls();

    }

    private void InsertBall()
    {

        float delta = Time.deltaTime * actualSpeed / spline.CalculateLength();

        
        
        trackedBall.Velocity = (expectedPosition - trackedBall.transform.position) * actualSpeed * 1.2f;

        for(int i = balls.Count - 1; i >= collisionSpot; i--)
        {

            Vector3 newPosition = spline.EvaluatePosition(ballOffsets[i] - delta);

            if((newPosition - balls[i - 1].transform.position).magnitude >= 2 * ballRadius)
            {
                balls[i].transform.position = newPosition;
                ballOffsets[i] -= delta;
            }
        }
        if(collisionSpot == 0 || collisionSpot == balls.Count || (balls[collisionSpot].transform.position - balls[collisionSpot - 1].transform.position).magnitude > 4 * ballRadius)
        {
            
            balls.Insert(collisionSpot, trackedBall);
            ballOffsets.Insert(collisionSpot, expectedOffset);
            
            collisionSpot = -1;
            trackedBall.State = BallState.InSnake;
            trackedBall = null;
        }
    }

    private float ExpectedOffset(int index)
    {
        if(index == 0)
        {
            return ballOffsets[0] + (ballOffsets[0] - ballOffsets[1]);
        }
        if(index == balls.Count)
        {
            return ballOffsets[balls.Count - 1] + (ballOffsets[balls.Count - 1] - ballOffsets[balls.Count - 2]);
        }
        return ballOffsets[index];
    }

    private int CheckCollision()
    {
        for(int i = 0; i < balls.Count; ++i)
        {
            float distance = (balls[i].transform.position - trackedBall.transform.position).magnitude;
            if(distance < 2 * ballRadius)
            {
                Plane p = new Plane(spline.EvaluateTangent(ballOffsets[i]), balls[i].transform.position);
                if(p.GetSide(trackedBall.transform.position))
                {
                    return i;
                }
                else
                {
                    return i+1;
                }
            }
        }
        return -1;
    }

    private void MoveBalls()
    {
        float delta = Time.deltaTime * actualSpeed / spline.CalculateLength();
        bool reversed = delta < 0;

        int start = reversed ? balls.Count - 1 : 0;
        int end = reversed ? -1 : balls.Count;
        for(int i = start; i != end; i+= reversed ? -1 : 1)
        {
            Vector3 newPosition = spline.EvaluatePosition(ballOffsets[i] + delta);

            if(    (!reversed  && i == 0)
                || ( reversed && i == balls.Count - 1)
                || (newPosition - balls[i - (reversed ? -1 : 1)].transform.position).magnitude >= 2 * ballRadius)
            {
                balls[i].transform.position = newPosition;
                ballOffsets[i] += delta;
                ballOffsets[i] = Mathf.Clamp(ballOffsets[i], 0, 1);
            }
        }



        if(balls.Count == 0 || ((balls.Last().transform.position - spawnPoint).magnitude > ballRadius && balls.Count < numOfBalls))
        {
            Ball go = Instantiate(ballTemplate, this.transform);
            go.transform.position = spawnPoint;
            go.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, colors.Count)];
            balls.Add(go);
            ballOffsets.Add(0);
        }
    }

    private void HighlightSelectedBalls()
    {
        if(balls.Count > 3)
        {
            for(int i = 0; i < balls.Count; i++)
            {

                Color col = balls[i].GetComponent<MeshRenderer>().material.color;
                float H, S, V;
                Color.RGBToHSV(col, out H, out S, out V);

                S = 0.5f;
                if(selection == i || selection + 1 == i)
                {
                    S = 1f;
                }

                balls[i].GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(H, S, V);

            }
        }
    }

    void HandleInput()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            actualSpeed = speed * speedUp;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            actualSpeed = speed * slowDown;
        }


        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selection++;
            selection = Mathf.Min(selection, balls.Count - 2);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            selection--;
            selection = Mathf.Max(selection, 1);
        }

        if(Input.GetKeyDown(KeyCode.Space) && balls.Count > 2)
        {
            Color temp = balls[selection].GetComponent<MeshRenderer>().material.color;
            balls[selection].GetComponent<MeshRenderer>().material.color = balls[selection + 1].GetComponent<MeshRenderer>().material.color;
            balls[selection + 1].GetComponent<MeshRenderer>().material.color = temp;
        }
    }

    public Ball GetRandomBall()
    {
        return balls.Count == 0 ? null : balls[Random.Range(0, balls.Count)];
    }

    public Color GetRandomColor()
    {
        return colors[Random.Range(0, colors.Count)];
    }

    public void TrackBall(Ball ball)
    {
        trackedBall = ball;
    }

    public bool Tracking()
    {
        return trackedBall != null;
    }
}


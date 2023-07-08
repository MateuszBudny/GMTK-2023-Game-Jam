using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    BallManager ballManager;

    [SerializeField]
    float coolDown = 5f;
    float currentCoolDown = 0f;


    [SerializeField]
    float speed = 2f;


    [SerializeField]
    GameObject ballTemplate;
    GameObject trackedBall;
 
    GameObject shootingBall;
    Vector3 shootingBallVelocity;
    Vector3 ballVelocity;
    Vector3 lastPosition;

    bool shooting = false;
    bool tracking = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(shooting)
        {
            shootingBall.transform.position += shootingBallVelocity * Time.deltaTime;
            if(!ballManager.Tracking())
            {
                shooting = false;
                tracking = true;
            }
        }

        if(tracking)
        {
            TrackChosenBall();
        }


        if(currentCoolDown > coolDown)
        {
            currentCoolDown = 0;


            trackedBall = null;

            shooting = true;
            tracking = false;
            shootingBallVelocity = this.transform.forward * speed;
            shootingBall = Instantiate(ballTemplate, this.transform.position, Quaternion.identity, ballManager.transform);
            ballManager.TrackBall(shootingBall);
        }

        
        if(tracking)
        {
            lastPosition = trackedBall.transform.position;
            currentCoolDown += Time.deltaTime;
        }
        
    }

    private void TrackChosenBall()
    {
        if(trackedBall == null)
        {
            trackedBall = ballManager.GetRandomBall();
            lastPosition = trackedBall.transform.position;
        }

        float distanceToBall = (this.transform.position - trackedBall.transform.position).magnitude;
        ballVelocity = (this.transform.position - lastPosition).normalized;
        this.transform.LookAt(trackedBall.transform.position + ballVelocity * distanceToBall / speed);

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Splines; 

public class BallManager : MonoBehaviour
{

    [SerializeField]
    SplineContainer spline;


    [SerializeField]
    int numOfBalls = 10;
    [SerializeField]
    float ballRadius = 1;
    [SerializeField]
    int speed = 5;

    float actualSpeed = 5;

    [SerializeField]
    GameObject ballTemplate;

    Vector3 spawnPoint = Vector3.zero;

    float currentTime = 0;
    GameObject head;
    List<GameObject> balls = new List<GameObject>();
    List<float> ballOffset = new List<float>();


    // Start is called before the first frame update
    void Start()
    {
        head = Instantiate(ballTemplate, this.transform);
        balls.Add(head);
        ballOffset.Add(0);

        spawnPoint = spline.EvaluatePosition(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            actualSpeed = speed * 2;
        }
        else
        {
            actualSpeed = speed;
        }
        float delta = Time.deltaTime/actualSpeed;

        head.transform.position = spline.EvaluatePosition(ballOffset[0] + delta);
        ballOffset[0] += delta;

        for (int i = 1; i < balls.Count; i++)
        {
            Vector3 newPosition = spline.EvaluatePosition(ballOffset[i] + delta);

            if ((newPosition - balls[i-1].transform.position).magnitude >= 2 * ballRadius)
            {
                balls[i].transform.position = newPosition;
                ballOffset[i] += delta;
            }
        }

        if((balls.Last().transform.position - spawnPoint).magnitude > ballRadius && balls.Count < numOfBalls)
        {
            GameObject go = Instantiate(ballTemplate, this.transform);
            go.transform.position = spawnPoint;
            balls.Add(go);
            ballOffset.Add(0);
        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Splines;

public class BallManager : SingleBehaviour<BallManager>
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

    [SerializeField]
    int minimumInRow = 3;

    Vector3 spawnPoint = Vector3.zero;


    int selection = 0;
    GameObject head;
    public List<Ball> balls = new List<Ball>();


    List<CollisionPackage> collisions = new List<CollisionPackage>();
    List<RetractionPackage> retractions = new List<RetractionPackage>();

    public SplineContainer Spline { get => spline; private set => spline = value; }


    // Start is called before the first frame update
    void Start()
    {
        selection = 1;

        spawnPoint = Spline.EvaluatePosition(0);
    }

    // Update is called once per frame
    void Update()
    {
        actualSpeed = speed;

        if(!isInputDisabled())
            HandleInput();

        if(isCollisionInProgress())
        {
            for(int i = collisions.Count - 1; i >= 0; --i)
            {
                CollisionPackage cp = collisions[i];
                if(cp.collisionSpot >= 0)
                    InsertBall(cp);
            }
            
        }
        else
        {
            MoveBalls();
        }

        foreach(CollisionPackage cp in collisions)
        {
            if(cp.trackedBall != null && cp.collisionSpot < 0)
            {
                cp.collisionSpot = CheckCollision(cp);
                if(cp.collisionSpot >= 0)
                {
                    cp.expectedOffset = ExpectedOffset(cp.collisionSpot);
                    cp.expectedPosition = Spline.EvaluatePosition(cp.expectedOffset);
                }
            }
        }


        //HighlightSelectedBalls();

    }

    private bool isCollisionInProgress()
    {
        foreach(CollisionPackage package in collisions)
        {
            if(package.collisionSpot >= 0)
                return true;
        }
        return false;
    }

    private bool isRetractionInProgress()
    {
        foreach(RetractionPackage package in retractions)
        {
            if(package.retractHead)
                return true;
        }
        return false;
    }

    private bool isInputDisabled()
    {
        return isCollisionInProgress() || isRetractionInProgress();
    }

    private void InsertBall(CollisionPackage cp)
    {

        float delta = Time.deltaTime * speed / Spline.CalculateLength();


        cp.trackedBall.Velocity = (cp.expectedPosition - cp.trackedBall.transform.position) * speed;

        for(int i = balls.Count - 1; i >= cp.collisionSpot; i--)
        {

            Vector3 newPosition = Spline.EvaluatePosition(balls[i].Progress - delta);

            if(i == balls.Count - 1 || (newPosition - balls[i + 1].transform.position).magnitude >= 2 * ballRadius)
            {
                balls[i].UpdateParameters(newPosition, newPosition - balls[i].transform.position, speed / Spline.CalculateLength());
                balls[i].Progress -= delta;
            }
        }
        if(cp.collisionSpot == 0 || cp.collisionSpot == balls.Count || (balls[cp.collisionSpot].transform.position - balls[cp.collisionSpot - 1].transform.position).magnitude > 4 * ballRadius)
        {

            balls.Insert(cp.collisionSpot, cp.trackedBall);
            balls[cp.collisionSpot].Progress = cp.expectedOffset;

            ClearRepeating(cp.collisionSpot);

            cp.trackedBall.State = BallState.InSnake;

            collisions.Remove(cp);
        }
    }

    private float ExpectedOffset(int index)
    {
        if(index == 0)
        {
            return balls[0].Progress + (balls[0].Progress - balls[1].Progress);
        }
        if(index == balls.Count)
        {
            return balls[balls.Count - 1].Progress + (balls[balls.Count - 1].Progress - balls[balls.Count - 2].Progress);
        }
        return balls[index].Progress;
    }

    private int CheckCollision(CollisionPackage cp)
    {
        for(int i = 0; i < balls.Count; ++i)
        {
            float distance = (balls[i].transform.position - cp.trackedBall.transform.position).magnitude;
            if(distance < 2 * ballRadius)
            {
                Plane p = new Plane(Spline.EvaluateTangent(balls[i].Progress), balls[i].transform.position);
                if(p.GetSide(cp.trackedBall.transform.position))
                {
                    return i;
                }
                else
                {
                    return i + 1;
                }
            }
        }
        return -1;
    }

    private void MoveBalls()
    {
        float delta = Time.deltaTime * actualSpeed / Spline.CalculateLength();
        bool reversed = delta < 0 || isRetractionInProgress();

        int start = reversed ? balls.Count - 1 : 0;
        int end = reversed ? -1 : balls.Count;
        RetractionPackage currentResolving = new RetractionPackage();
        foreach(RetractionPackage rp in retractions)
        {
            if(rp.retractHead)
            {
                if(rp.retractIndex - 1 < start)
                {
                    start = rp.retractIndex - 1;
                    currentResolving = rp;

                }

            }

        }
        delta = isRetractionInProgress() ? -delta : delta;
        

        for(int i = start; i != end; i += reversed ? -1 : 1)
        {
            Vector3 newPosition = Spline.EvaluatePosition(balls[i].Progress + delta);

            if((!reversed && i == 0)
                || (reversed && i == balls.Count - 1)
                || (newPosition - balls[i - (reversed ? -1 : 1)].transform.position).magnitude >= 2 * ballRadius)
            {
                balls[i].UpdateParameters(newPosition, newPosition - balls[i].transform.position, actualSpeed / Spline.CalculateLength());
                balls[i].Progress += delta;
                balls[i].Progress = Mathf.Clamp(balls[i].Progress, 0, 1);
            }

            if(isRetractionInProgress() 
                && i == start 
                && (newPosition - balls[i - (reversed ? -1 : 1)].transform.position).magnitude < 2 * ballRadius)
            {
                
                if(balls[i].Color == balls[i+1].Color)
                {
                    ClearRepeating(start);
                }

                retractions.Remove(currentResolving);
            }
        }



        if(balls.Count == 0 || ((balls.Last().transform.position - spawnPoint).magnitude > ballRadius && 0 < numOfBalls))
        {
            Ball go = Instantiate(ballTemplate, this.transform);
            go.UpdateParameters(spawnPoint, Spline.EvaluateTangent(0), actualSpeed / Spline.CalculateLength());
            go.Color = colors[Random.Range(0, colors.Count)];
            balls.Add(go);
            balls.Last().Progress = 0;
            numOfBalls--;
        }
    }

    private void HighlightSelectedBalls()
    {
        if(balls.Count > 3)
        {
            for(int i = 0; i < balls.Count; i++)
            {

                Color col = balls[i].Color;
                float H, S, V;
                Color.RGBToHSV(col, out H, out S, out V);

                S = 0.5f;
                if(selection == i || selection + 1 == i)
                {
                    S = 1f;
                }

                balls[i].Color = Color.HSVToRGB(H, S, V);

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
            selection = Mathf.Min(selection, balls.Count - 1);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            selection--;
            selection = Mathf.Max(selection, 0);
        }

        if(Input.GetKeyDown(KeyCode.Space) && balls.Count > 2)
        {
            Color temp = balls[selection].Color;
            balls[selection].Color = balls[selection + 1].Color;
            balls[selection + 1].Color = temp;

            ClearRepeating(selection);
        }
    }

    private void ClearRepeating(int pos)
    {
        int end = pos;
        int start = pos;
        Color currentColor = balls[pos].Color;

        while(end < balls.Count && balls[end].Color == currentColor)
            end++;
        while(start >= 0 && balls[start].Color == currentColor)
            start--;

        start++;
        end--;

        if(end - start + 1 >= minimumInRow)
        {
            if(start != 0 && end != balls.Count - 1)
            {
                RetractionPackage rp = new RetractionPackage();
                rp.retractHead = true;
                rp.retractIndex = start;
                retractions.Add(rp);
            }
            for(int j = 0; j < end - start + 1; j++)
            {
                Destroy(balls[start].gameObject);
                balls.RemoveAt(start);
            }
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
        CollisionPackage cp = new CollisionPackage();
        cp.trackedBall = ball;
        collisions.Add(cp);
    }

    public void UntrackBall(Ball ball)
    {
    }

    public bool Tracking()
    {
        return collisions.Count > 0;
    }
}


class CollisionPackage
{
    public Ball trackedBall;
    public float expectedOffset;
    public Vector3 expectedPosition;
    public int collisionSpot = -1;
    public bool blocksInput() { return collisionSpot > -1; }
}

class RetractionPackage
{
    public bool retractHead = false;
    public int retractIndex = -1;


    public bool blocksInput() { return retractHead; }
}
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private BallManager ballManager;

    [SerializeField]
    private float shootingCooldown = 5f;

    [SerializeField]
    private float ballStartingSpeed = 2f;

    [SerializeField]
    private Ball ballTemplate;

    private float currentCoolDown = 0f;
    private Ball trackedBall;
    private Vector3 ballVelocity;
    private Vector3 lastPosition;

    private void Update()
    {
        TrackChosenBall();
        CheckIfShouldShoot();
        CooldownElapsing();
    }

    private void ShootBall()
    {
        Ball shootingBall = Instantiate(ballTemplate, transform.position, Quaternion.identity, ballManager.transform);
        shootingBall.State = BallState.Flying;
        shootingBall.Velocity = transform.forward * ballStartingSpeed;
        ballManager.TrackBall(shootingBall);
    }

    private void CheckIfShouldShoot()
    {
        if(currentCoolDown > shootingCooldown)
        {
            currentCoolDown = 0;
            trackedBall = null;

            ShootBall();
        }
    }

    private void TrackChosenBall()
    {
        if(trackedBall == null)
        {
            trackedBall = ballManager.GetRandomBall().GetComponent<Ball>();
        }

        lastPosition = trackedBall.transform.position;
        float distanceToBall = (transform.position - trackedBall.transform.position).magnitude;
        ballVelocity = (transform.position - lastPosition).normalized;
        transform.LookAt(trackedBall.transform.position + ballVelocity * distanceToBall / ballStartingSpeed);
    }

    private void CooldownElapsing()
    {
        currentCoolDown += Time.deltaTime;
    }
}

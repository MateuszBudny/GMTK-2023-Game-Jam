using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private BallManager ballManager;
    [SerializeField]
    private float shootingCooldown = 5f;
    [SerializeField]
    private float shootingBallSpawnDelayAfterShoot = 1f;
    [SerializeField]
    private float ballStartingSpeed = 2f;
    [SerializeField]
    private Ball ballTemplate;
    [SerializeField]
    private float maxRotationSpeed = 1f;
    [SerializeField]
    private Transform ballSpawnPoint;

    [BoxGroup("Starting Modules")]
    [SerializeField]
    private ShooterTracking startingTracker;
    [BoxGroup("Starting Modules")]
    [SerializeField]
    private ShooterChoosingBallToTrack startingChooser;

    private Ball ballSpawnedWaitingForShoot;
    private float currentCooldown;
    private float targetYAngle;

    private void Start()
    {
        StartCoroutine(LoadNewBallAfterDelay());
    }

    private void Update()
    {
        TrackChosenBall();
        CheckIfShouldShoot();
        SmoothlyRotateIfNeeded();
        CooldownElapsing();
    }

    private void ShootBall()
    {
        ballSpawnedWaitingForShoot.State = BallState.Flying;
        ballSpawnedWaitingForShoot.Velocity = transform.forward * ballStartingSpeed;
        ballSpawnedWaitingForShoot.transform.parent = ballManager.transform;
        ballManager.TrackBall(ballSpawnedWaitingForShoot);
        ballSpawnedWaitingForShoot = null;

        StartCoroutine(LoadNewBallAfterDelay());
    }

    private void CheckIfShouldShoot()
    {
        if(currentCooldown > shootingCooldown)
        {
            currentCooldown = 0;
            startingTracker.TrackedBall = null;

            ShootBall();
        }
    }

    private void TrackChosenBall()
    {
        if(startingTracker.TrackedBall == null)
        {
            startingTracker.TrackedBall = startingChooser.GetBallToTrack();
        }

        if(startingTracker.TrackedBall != null)
        {
            Vector3 shooterLookAtPos = startingTracker.GetShooterLookAtPosition(ballStartingSpeed, ballSpawnPoint.position);
            Vector3 direction = shooterLookAtPos - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetYAngle = targetRotation.eulerAngles.y;
        }
    }

    private void SmoothlyRotateIfNeeded()
    {
        if(!targetYAngle.Approximately(transform.rotation.eulerAngles.y))
        {
            float newYRotation = Mathf.MoveTowards(transform.rotation.eulerAngles.y, targetYAngle, maxRotationSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, newYRotation, transform.rotation.eulerAngles.z));
        }
    }

    private void CooldownElapsing()
    {
        currentCooldown += Time.deltaTime;
    }

    private IEnumerator LoadNewBallAfterDelay()
    {
        yield return new WaitForSeconds(shootingBallSpawnDelayAfterShoot);
        SpawnBallToShoot();
    }

    private void SpawnBallToShoot()
    {
        ballSpawnedWaitingForShoot = Instantiate(BallManager.Instance.GetBallTemplateOfRandomColor(), ballSpawnPoint.position, Quaternion.identity, transform);
        ballSpawnedWaitingForShoot.State = BallState.SpawnedWaitingForShoot;
    }
}

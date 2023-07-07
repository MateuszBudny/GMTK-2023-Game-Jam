using Cinemachine;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    [SerializeField]
    private Volume volume;

    [Header("Vignette")]
    [SerializeField]
    [Range(0f, 1f)]
    private float maxVignetteValue = 1f;
    [SerializeField]
    private float vignetteDefaultChangeDuration = 1.5f;
    [Header("Camera shake")]
    [SerializeField]
    private CinemachineNoiseMovement vCamNoiseMovement;

    private Vignette vignette;

    private void Start()
    {
        volume.profile.TryGet(out vignette);
    }

    public void Init(Func<CinemachineVirtualCamera> getCurrentVCam)
    {
        vCamNoiseMovement.Init(getCurrentVCam);
    }

    public void SetVignetteSmoothlyAsMaxPercentage(float endValueAsMaxPercentage, float duration = -1f) => SetVignetteSmoothly(endValueAsMaxPercentage * maxVignetteValue, duration);

    public void SetVignetteSmoothly(float endValue, float duration = -1f)
    {
        duration = Mathf.Approximately(duration, -1f) ? vignetteDefaultChangeDuration : duration; 
        DOTween.To(() => vignette.intensity.value, SetVignette, endValue, duration);
    }

    public void SetVignette(float value)
    {
        vignette.intensity.value = value;
    }

    public void SetVCamNoiseMovementProgress(float progress)
    {
        vCamNoiseMovement.UpdateProgress(progress);
    }
}

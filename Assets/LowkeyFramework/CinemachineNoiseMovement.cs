using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineNoiseMovement : MonoBehaviour, INoiseMovement
{
    [SerializeField]
    private ValueProgressedByOffset amplitudeGainOffsetProgression;
    [SerializeField]
    private ValueProgressedByOffset frequencyGainOffsetProgression;

    private CinemachineBasicMultiChannelPerlin VCamNoise
    {
        get
        {
            if (getCurrentVCam != null && getCurrentVCam())
            {
                return getCurrentVCam().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }

            return null;
        }
    }

    private float CurrentProgress
    {
        get => currentProgress;
        set
        {
            currentProgress = value;
            if(VCamNoise)
            {
                VCamNoise.m_AmplitudeGain = amplitudeGainOffsetProgression.GetValue(CurrentProgress);
                VCamNoise.m_FrequencyGain = frequencyGainOffsetProgression.GetValue(CurrentProgress);
            }
        }
    }

    private Func<CinemachineVirtualCamera> getCurrentVCam;
    private float currentProgress;

    public void Init(Func<CinemachineVirtualCamera> getCurrentVCam)
    {
        this.getCurrentVCam = getCurrentVCam;
    }

    public void UpdateGainOffsetsOnVCamChanged(CinemachineVirtualCamera _)
    {
        UpdateProgress(CurrentProgress);
    }

    public void UpdateProgress(float noiseProgress) => CurrentProgress = noiseProgress;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public static class MathUtils
{
    public static float RecalculateAngleToBetween0And360(float angle)
    {
        float recalculatedAngle = angle % 360;
        recalculatedAngle = recalculatedAngle >= 0 ? recalculatedAngle : 360 + recalculatedAngle;

        return recalculatedAngle;
    }

    public static float RecalculateAngleToBetweenMinus180And180(float angle)
    {
        float recalculatedAngle = angle % 360;
        recalculatedAngle = recalculatedAngle > 180 ? recalculatedAngle - 360 : recalculatedAngle;
        recalculatedAngle = recalculatedAngle < -180 ? recalculatedAngle + 360 : recalculatedAngle;

        return recalculatedAngle;
    }

    public static Vector3 RandomSmoothOffsetNoise3D(float amplitude = 1f, float constFrequency = 1f, float xSeed = 0f, float ySeed = 1f, float zSeed = 2f) => RandomSmoothOffsetNoise3D(Time.time, amplitude, constFrequency, xSeed, ySeed, zSeed);

    public static Vector3 RandomSmoothOffsetNoise3D(float time, float amplitude = 1f, float constFrequency = 1f, float xSeed = 0f, float ySeed = 1f, float zSeed = 2f)
    {
        float adjustedTime = time * constFrequency;
        return new Vector3(
            GetSingleAxisNoiseValue(xSeed),
            GetSingleAxisNoiseValue(ySeed),
            GetSingleAxisNoiseValue(zSeed));

        float GetSingleAxisNoiseValue(float axisSeed)
        {
            return (Mathf.PerlinNoise(adjustedTime, axisSeed) - 0.5f) * amplitude;
        }
    }
}

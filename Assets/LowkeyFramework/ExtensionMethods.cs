//using LowkeyFramework.AttributeSaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector2 Rotate(this Vector2 vector, float eulerAngle)
    {
        float radians = eulerAngle * Mathf.Deg2Rad;
        return new Vector2(
            vector.x * Mathf.Cos(radians) - vector.y * Mathf.Sin(radians),
            vector.x * Mathf.Sin(radians) + vector.y * Mathf.Cos(radians)
        );
    }

    public static Vector3 RotateAroundPivot(this Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    public static Vector2 RotateAroundPivot(Vector2 point, Vector2 pivot, float angle)
    {
        return (point - pivot).Rotate(angle) + pivot;
    }

    public static T GetRandomElement<T>(this List<T> list)
    {
        int chosenElementIndex = UnityEngine.Random.Range(0, list.Count);
        return list[chosenElementIndex];
    }

    public static Vector3 RandomRange(this Vector3 minInclusive, Vector3 maxInclusive)
    {
        return new Vector3(UnityEngine.Random.Range(minInclusive.x, maxInclusive.x), UnityEngine.Random.Range(minInclusive.y, maxInclusive.y), UnityEngine.Random.Range(minInclusive.z, maxInclusive.z));
    }

    //public static void SafeDestroy(this GameObject gameObject)
    //{
    //    gameObject.transform.parent = null;
    //    gameObject.name = "$disposed";
    //    SaveableBehaviour saveable = gameObject.GetComponent<SaveableBehaviour>();
    //    if(saveable)
    //    {
    //        saveable.TurnOffSavingAndLoadingForThisBehaviour = true;
    //    }
    //    UnityEngine.Object.Destroy(gameObject);
    //    gameObject.SetActive(false);
    //}
}

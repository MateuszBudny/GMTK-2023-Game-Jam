using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantGlobalRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 constantRotationValues = Vector2.zero;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(constantRotationValues);
    }
}

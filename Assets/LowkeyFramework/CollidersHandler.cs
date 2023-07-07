using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollidersHandler : MonoBehaviour
{
    [SerializeField]
    private bool autoCollidersDetection = false;
    public List<Collider> colliders;

    private void Start()
    {
        if(autoCollidersDetection)
        {
            colliders = GetComponentsInChildren<Collider>().ToList();
        }
    }

    public void CollidersSetEnabled(bool enabled)
    {
        colliders.ForEach(collider => collider.enabled = enabled);
    }
}

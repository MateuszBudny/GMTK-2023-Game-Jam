//using LowkeyFramework.AttributeSaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBehaviour<T> : MonoBehaviour where T : SingleBehaviour<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if(Instance == null)
        {
            OnFirstInstance();   
        }
        else
        {
            OnAnotherInstance();
            if(gameObject.activeInHierarchy)
            {
                StartCoroutine(OnAnotherInstanceEnumerator());
            }
        }
    }

    protected virtual void OnFirstInstance()
    {
        Instance = this as T;
    }

    protected virtual void OnAnotherInstance() {}

    protected virtual IEnumerator OnAnotherInstanceEnumerator()
    {
        yield return null;
        Debug.LogError($"More than one SingleBehaviour of the same class is present on the scene, even after one frame. Additional SingleBehaviour's GameObject name: {name}");
    }
}

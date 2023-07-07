using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private CursorLockMode cursorLockMode = CursorLockMode.Locked;

    private void Start()
    {
        Cursor.lockState = cursorLockMode;
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus)
        {
            Cursor.lockState = cursorLockMode;
        }
    }
}

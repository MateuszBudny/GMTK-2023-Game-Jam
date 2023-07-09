using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : SingleBehaviour<RestartManager>
{
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            Restart();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

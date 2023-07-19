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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

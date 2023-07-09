using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string Lvl1SceneName = "LVL1";

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(Lvl1SceneName);
    }
}

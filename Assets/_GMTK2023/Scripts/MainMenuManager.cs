using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string Lvl1SceneName = "LVL1";
    [SerializeField]
    private AudioClip buttonClickSound;

    public void OnStartButtonClick()
    {
        SoundManager.Instance.PlayEnvironmentSound(buttonClickSound);
        SceneManager.LoadScene(Lvl1SceneName);
    }
}

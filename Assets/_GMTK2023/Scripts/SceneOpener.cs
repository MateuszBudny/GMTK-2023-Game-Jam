using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOpener : MonoBehaviour
{
    [SerializeField]
    private string sceneToOpen = "LVL1";
    [SerializeField]
    private AudioClip buttonClickSound;

    public void OpenScene()
    {
        SoundManager.Instance.PlayEnvironmentSound(buttonClickSound);
        SceneManager.LoadScene(sceneToOpen);
    }
}

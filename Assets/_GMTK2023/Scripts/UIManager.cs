using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingleBehaviour<UIManager>
{
    [SerializeField]
    private GameObject youLoseScreen;
    [SerializeField]
    private GameObject youWinScreen;
    [SerializeField]
    private string nextLvlName;

    public void OnYouLose()
    {
        youLoseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void YouLoseClick()
    {
        RestartManager.Instance.Restart();
    }

    public void OnYouWin()
    {
        youWinScreen.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void YouWinClick()
    {
        SceneManager.LoadScene(nextLvlName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject mainButtons;
    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowHelp()
    {
        infoPanel.SetActive(true);
        mainButtons.SetActive(false);
    }
    public void HideHelp()
    {
        infoPanel.SetActive(false);
        mainButtons.SetActive(true);
    }
}

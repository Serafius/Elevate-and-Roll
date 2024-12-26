using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
     public void LoadSettings()
    {
        SceneManager.LoadScene(6);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

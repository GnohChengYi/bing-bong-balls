using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Audio.InitializeClips();
    }

    public void PlayFreePlay()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayPuzzle()
    {
        SceneManager.LoadScene(2);
    }

    public void ShowHighScores()
    {
        SceneManager.LoadScene(3);
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

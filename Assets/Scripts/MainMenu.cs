using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayFreePlay ()
    {
        SceneManager.LoadScene(1);
    }

    // public void PlayPuzzle ()
    // {
    //     SceneManager.LoadScene(2);
    // }

    public void QuitGame ()
    {
        Application.Quit();
    }
}

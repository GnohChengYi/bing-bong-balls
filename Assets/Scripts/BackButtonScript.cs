using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    public void BackToStartMenu()
    {
        SceneManager.LoadScene(0);
        Reset();
    }

    private void Reset()
    {
        // TODO check whether need reset GameManager
        Puzzle.selectedPuzzle = null;
    }
}
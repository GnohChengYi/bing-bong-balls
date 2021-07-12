using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPuzzle : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPuzzleButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
}

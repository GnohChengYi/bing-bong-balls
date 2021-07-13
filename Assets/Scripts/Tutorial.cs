using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // TODO update total tutorials accordingly
    private const int total = 3;
    private int completed = 0;

    [SerializeField]
    private Image image;

    private void Start()
    {
        DisplayCurrentTutorial();
    }

    public void Next()
    {
        if (completed < total)
        {
            completed++;
            DisplayCurrentTutorial();
        }
        else SceneManager.LoadScene(0);
    }

    private void DisplayCurrentTutorial()
    {
        string path = "Tutorials/Tutorial" + completed.ToString();
        Sprite nextSprite = Resources.Load<Sprite>(path);
        image.sprite = nextSprite;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private const int lastIndex = 15;
    private int current;

    [SerializeField]
    private Image image;

    private void Start()
    {
        current = 0;
        DisplayCurrentTutorial();
    }

    public void Next()
    {
        if (current < lastIndex)
        {
            current++;
            DisplayCurrentTutorial();
        }
        else SceneManager.LoadScene(0);
    }

    private void DisplayCurrentTutorial()
    {
        string path = "Tutorials/Tutorial" + current.ToString();
        Sprite nextSprite = Resources.Load<Sprite>(path);
        image.sprite = nextSprite;
    }
}

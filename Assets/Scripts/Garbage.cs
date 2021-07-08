using UnityEngine;
using UnityEngine.UI;

// TODO make garbage image red when drag onto it, not working yet
public class Garbage : MonoBehaviour
{
    [SerializeField]
    private Image garbageImage;

    private Color blackishRed;

    private void Start()
    {
        blackishRed = Color.Lerp(Color.red, Color.black, 0.3f);
    }

    public static Garbage Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void TurnRed()
    {
        garbageImage.color = blackishRed;
    }

    public void TurnBlack()
    {
        garbageImage.color = Color.black;
    }
}

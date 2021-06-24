using UnityEngine;
using UnityEngine.UI;

// Attach this script to GameObjects that need to create toast
public class Toast : MonoBehaviour
{
    [SerializeField]
    private GameObject toastButton;
    private Image background;

    [SerializeField]
    private GameObject toastTextObject;
    private Text toastTextComponent;

    private void Start()
    {
        background = toastButton.GetComponent<Image>();
        toastTextComponent = toastTextObject.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        // TODO make fading smoother
        background.color = Color.Lerp(
            background.color, Color.clear, Time.deltaTime);
        toastTextComponent.color = Color.Lerp(
            toastTextComponent.color, Color.clear, Time.deltaTime);
        Debug.Log(background.color.a);
        if (background.color.a < 0.01) DestroyToast();
    }

    public void DestroyToast()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    private float time;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Update()
    {
        time += Time.deltaTime;
        if (time <= 1.25) FadeIn();
        else if (time <= 2.5) FadeOut();
        else time = 0;
    }

    private void FadeIn()
    {
        canvasGroup.alpha = canvasGroup.alpha * 9 / 10 + 0.1f;
    }

    private void FadeOut()
    {
        canvasGroup.alpha *= 9f / 10;
    }
}

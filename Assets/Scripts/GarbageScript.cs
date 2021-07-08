using UnityEngine;
using UnityEngine.UI;

// TODO make garbage image red when drag onto it, not working yet
public class GarbageScript : MonoBehaviour
{
    [SerializeField]
    private Image garbageImage;

    private void OnMouseEnter()
    {
        TurnRed();
    }

    private void OnMouseExit()
    {
        TurnBlack();
    }

    private void OnEnable()
    {
        TurnBlack();
    }


    public void TurnRed()
    {
        garbageImage.color = Color.red;
    }

    public void TurnBlack()
    {
        garbageImage.color = Color.black;
    }
}

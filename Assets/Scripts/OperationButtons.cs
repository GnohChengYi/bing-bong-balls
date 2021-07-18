using UnityEngine;
using UnityEngine.UI;

public class OperationButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject LauncherButton;
    [SerializeField]
    private GameObject AddBlockButton;
    [SerializeField]
    private GameObject SelectButton;

    public void OnCreateLauncherClick()
    {
        GameManager.Instance.operation = Operation.CREATE_LAUNCHER;
        GameManager.Instance.currentElement = null;
        LauncherButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Add Launcher Mode Button");
        AddBlockButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Add Block Button");
        SelectButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Select Button");
    }

    public void OnCreateBlockClick()
    {
        GameManager.Instance.operation = Operation.CREATE_BLOCK;
        GameManager.Instance.currentElement = null;
       LauncherButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Add Launcher Button");
        AddBlockButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Add Block Mode Button");
        SelectButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Select Button");
    }

    public void OnSelectClick()
    {
        GameManager.Instance.operation = Operation.SELECT;
        GameManager.Instance.currentElement = null;
        LauncherButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Add Launcher Button");
        AddBlockButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Add Block Button");
        SelectButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Select Mode Button");
    }
}

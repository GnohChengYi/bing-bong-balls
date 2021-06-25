using UnityEngine;

public class OperationButtons : MonoBehaviour
{
    public void OnCreateLauncherClick()
    {
        GameManager.Instance.operation = Operation.CREATE_LAUNCHER;
        GameManager.Instance.currentElement = null;
    }

    public void OnCreateBlockClick()
    {
        GameManager.Instance.operation = Operation.CREATE_BLOCK;
        GameManager.Instance.currentElement = null;
    }

    public void OnSelectClick()
    {
        GameManager.Instance.operation = Operation.SELECT;
        GameManager.Instance.currentElement = null;
    }
}

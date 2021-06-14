using UnityEngine;

public class OperationButtons : MonoBehaviour
{
    public void OnCreateLauncherClick()
    {
        GameManager.Instance.operation = Operation.CREATE_LAUNCHER;
    }

    public void OnCreateBlockClick()
    {
        GameManager.Instance.operation = Operation.CREATE_BLOCK;
    }

    public void OnSelectClick()
    {
        GameManager.Instance.operation = Operation.SELECT;
    }
}

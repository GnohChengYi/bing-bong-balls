using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public void OnCreateLauncherClick()
    {
        GameManager.Instance.operation = Operation.CREATE_LAUNCHER;
    }

    public void OnCreateBlockClick()
    {
        GameManager.Instance.operation = Operation.CREATE_BLOCK;
    }

    public void OnMoveClick()
    {
        GameManager.Instance.operation = Operation.SELECT;
    }
}

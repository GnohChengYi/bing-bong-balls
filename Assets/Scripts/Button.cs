using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnCreateLauncherClick()
    {
        GameManager.Instance.mode = GameManager.Mode.CREATE_LAUNCHER;
        Debug
            .Log(GameManager.Instance.mode == GameManager.Mode.CREATE_LAUNCHER);
    }

    public void OnCreateBlockClick()
    {
        GameManager.Instance.mode = GameManager.Mode.CREATE_BLOCK;
        Debug.Log(GameManager.Instance.mode == GameManager.Mode.CREATE_BLOCK);
    }

    public void OnMoveClick()
    {
        GameManager.Instance.mode = GameManager.Mode.MOVE;
        Debug.Log(GameManager.Instance.mode == GameManager.Mode.MOVE);
    }

    public void OnRecordingClick()
    {
        GameManager.Instance.mode = GameManager.Mode.RECORDING;
        Debug.Log(GameManager.Instance.mode == GameManager.Mode.RECORDING);
    }
}

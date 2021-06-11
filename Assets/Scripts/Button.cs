using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private Text recordingText;

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

    public void OnRecordingClick()
    {
        if (recordingText == null)
            recordingText = GetComponentInChildren<Text>();

        GameManager.Instance.operation = Operation.SELECT;

        // TODO improve code if needed (e.g. use enum or change button image)
        if (!GameManager.Instance.isRecording)
        {
            Debug.Log("not recording, record now");
            GameManager.Instance.isRecording = true;
            recordingText.text = "Stop\nRecording";
        }
        else
        {
            Debug.Log("recording, stop now");
            GameManager.Instance.isRecording = false;
            GameManager.Instance.ShowSaveDialog();
            recordingText.text = "Start\nRecording";
        }
    }
}

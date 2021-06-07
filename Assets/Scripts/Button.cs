using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private Text recordingText;

    public void OnCreateLauncherClick()
    {
        GameManager.Instance.mode = GameManager.Mode.CREATE_LAUNCHER;
    }

    public void OnCreateBlockClick()
    {
        GameManager.Instance.mode = GameManager.Mode.CREATE_BLOCK;
    }

    public void OnMoveClick()
    {
        GameManager.Instance.mode = GameManager.Mode.SELECT;
    }

    public void OnRecordingClick()
    {
        if (recordingText == null)
            recordingText = GetComponentInChildren<Text>();

        GameManager.Instance.mode = GameManager.Mode.SELECT;

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

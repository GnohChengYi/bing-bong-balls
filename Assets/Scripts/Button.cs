using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private Text recordingText;

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
    }

    public void OnCreateBlockClick()
    {
        GameManager.Instance.mode = GameManager.Mode.CREATE_BLOCK;
    }

    public void OnMoveClick()
    {
        GameManager.Instance.mode = GameManager.Mode.MOVE;
    }

    public void OnRecordingClick()
    {
        GameManager.Instance.mode = GameManager.Mode.RECORDING;
        if (recordingText == null)
            recordingText = GetComponentInChildren<Text>();

        // TODO improve code (e.g. use enum)
        if (recordingText.text == "Start\nRecording")
            recordingText.text = "Stop\nRecording";
        else if (recordingText.text == "Stop\nRecording")
            recordingText.text = "Start\nRecording";
    }
}

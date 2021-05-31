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
        if (recordingText == null)
            recordingText = GetComponentInChildren<Text>();

        GameManager.Instance.mode = GameManager.Mode.MOVE;

        // TODO improve code (e.g. use enum or change button image)
        if (!GameManager.Instance.isRecording)
        {
            GameManager.Instance.isRecording = true;
            recordingText.text = "Start\nRecording";
            GameManager.Instance.SaveAudio();
        }
        else
        {
            GameManager.Instance.isRecording = false;
            recordingText.text = "Stop\nRecording";
        }
    }
}

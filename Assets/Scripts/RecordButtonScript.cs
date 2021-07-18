using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RecordButtonScript : MonoBehaviour
{
    [SerializeField]
    private Sprite RecordImg;
    [SerializeField]
    private Sprite StopImg;

    [SerializeField]
    private GameObject LauncherButton;
    [SerializeField]
    private GameObject AddBlockButton;
    [SerializeField]
    private GameObject SelectButton;

    private Image image;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void OnRecordingClick()
    {
        GameManager.Instance.operation = Operation.SELECT;
        LauncherButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Add Launcher Button");
        AddBlockButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Add Block Button");
        SelectButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Select Mode Button");
        if (!GameManager.Instance.isRecording)
        {
            image.overrideSprite = StopImg;
            GameManager.Instance.isRecording = true;
            if (GameManager.Instance.mode == Mode.PUZZLE)
                Puzzle.submission.Clear();
        }
        else
        {
            image.overrideSprite = RecordImg;
            GameManager.Instance.isRecording = false;
            if (GameManager.Instance.mode == Mode.FREE_PLAY)
                GameManager.Instance.ShowSaveDialog();
            else if (GameManager.Instance.mode == Mode.PUZZLE)
                GameManager.Instance.HandlePuzzleSubmission();
        }
    }
}

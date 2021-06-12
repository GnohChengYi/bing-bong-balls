using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RecordButtonScript : MonoBehaviour
{
    public Sprite RecordImg;
    public Sprite StopImg;
    // public Button button;

    public void OnRecordingClick()
    {
        GameManager.Instance.operation = Operation.SELECT;

        // TODO improve code if needed (e.g. use enum or change button image)
        if (!GameManager.Instance.isRecording)
        {
            // button.image.overrideSprite = StopImg;
            gameObject.GetComponent<Image>().overrideSprite = StopImg;
            Debug.Log("not recording, record now");
            GameManager.Instance.isRecording = true;
        }
        else
        {
            // button.image.overrideSprite = RecordImg;
            gameObject.GetComponent<Image>().overrideSprite = RecordImg;
            Debug.Log("recording, stop now");
            GameManager.Instance.isRecording = false;
            GameManager.Instance.ShowSaveDialog();
        }
    }
}

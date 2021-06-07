using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDialog : MonoBehaviour
{
    [SerializeField]
    private InputField filenameField;

    public void OnSaveClick()
    {
        string filename = filenameField.text;
        GameManager.Instance.SaveAudio(filename);
        gameObject.SetActive(false);
    }

    public void OnCancelClick()
    {
        GameManager.Instance.CancelAudio();
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject guideToggleObject;
    private Toggle guideToggle;

    public AudioMixer audioMixer;

    private void Start()
    {
        guideToggle = guideToggleObject.GetComponent<Toggle>();
        guideToggle.isOn = GuideController.GetGuidePref();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void UpdateGuidePref()
    {
        GuideController.SetGuidePref(guideToggle.isOn);
    }
}

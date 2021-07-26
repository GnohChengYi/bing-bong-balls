using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public void SetVolume(float volume)
    {
        BackgroundMusic.Instance.audioSource.volume = volume;
    }
}

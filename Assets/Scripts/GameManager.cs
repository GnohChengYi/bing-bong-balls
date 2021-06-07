using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Firebase.Leaderboard;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Mode
    {
        CREATE_LAUNCHER,
        CREATE_BLOCK,
        SELECT
    }

    public Mode mode;

    // auto deselect when click on Panel, so need to select back last selected Launcher or Block
    public Selectable lastSelected;

    // selected launcher, null when no launcher selected;
    public Launcher launcher;

    public bool isRecording;

    private List<float> audioDataList;

    [SerializeField]
    private GameObject saveDialog;

    private AudioClip recordingClip;

    private LeaderboardController leaderboard;

    // TODO fix screen orientation
    void Start()
    {
        mode = Mode.SELECT;
        leaderboard = GetComponent<LeaderboardController>();
        audioDataList = new List<float>();
    }

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void OnAudioFilterRead(float[] data, int channels)
    {
        if (isRecording)
        {
            audioDataList.AddRange (data);
        }
    }

    public void ShowSaveDialog()
    {
        // TODO disable (set enabled=false or sth) touch on simulation panel and toolbar
        saveDialog.SetActive(true);
    }

    public void SaveAudio(string filename)
    {
        float[] audioData = audioDataList.ToArray();
        int channels = 2;
        int lengthSamples = audioData.Length / channels;
        int frequency = 44100;
        recordingClip =
            AudioClip
                .Create("Recording", lengthSamples, channels, frequency, false);
        recordingClip.SetData(audioData, 0);
        string filepath = Application.persistentDataPath + "/" + filename;
        int bitDepth = 16;
        int bitRate = frequency * bitDepth * channels;
        EncodeMP3.convert (recordingClip, filepath, bitRate);
        // TODO show toast save directory
        CancelAudio();
    }

    public void CancelAudio()
    {
        audioDataList.Clear();
    }
}

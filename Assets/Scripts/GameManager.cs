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

    public AudioRenderer audioRenderer;

    private List<float> audioDataList;

    private AudioClip recordingClip;

    private LeaderboardController leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        mode = Mode.SELECT;
        leaderboard = GetComponent<LeaderboardController>();
        audioRenderer = new AudioRenderer();
        audioRenderer.rendering = true;
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
            // audioRenderer.OnAudioFilterRead (data, channels); // front or back blank in wav smh
        }
    }

    // TODO handle custom directory (including filename)
    public void SaveAudio()
    {
        // string filepath = Application.persistentDataPath + "/temp_music.wav";
        // audioRenderer.Save (filepath);
        // audioRenderer.Clear();
        float[] audioData = audioDataList.ToArray();
        int channels = 2;
        int lengthSamples = audioData.Length / channels;
        int frequency = 44100;
        recordingClip =
            AudioClip
                .Create("Recording", lengthSamples, channels, frequency, false);
        recordingClip.SetData(audioData, 0);
        string filepath = Application.persistentDataPath + "/temp_music.mp3";
        int bitDepth = 16;
        int bitRate = frequency * bitDepth * channels;
        EncodeMP3.convert (recordingClip, filepath, bitRate);
    }
}

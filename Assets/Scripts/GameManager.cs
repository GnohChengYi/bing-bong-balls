using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Firebase.Leaderboard;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Mode mode;

    [SerializeField]
    private GameObject listenPuzzleButton;

    public Operation operation;

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
    private void Start()
    {
        leaderboard = GetComponent<LeaderboardController>();
    }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private void OnRenderObject()
    {
        if (Puzzle.selectedPuzzle == null)
        {
            mode = Mode.FREE_PLAY;
            listenPuzzleButton.SetActive(false);
        }
        else
        {
            mode = Mode.PUZZLE;
            listenPuzzleButton.SetActive(true);
        }
        operation = Operation.SELECT;
        audioDataList = new List<float>();
    }

    public void OnAudioFilterRead(float[] data, int channels)
    {
        if (isRecording) audioDataList.AddRange(data);

    }

    public void ShowSaveDialog()
    {
        // TODO disable (set enabled=false or sth) touch on simulation panel and toolbar
        saveDialog.SetActive(true);
    }

    public void SaveAudio(string filename)
    {
        int channels = 2;
        int frequency = 44100;
        AudioClip recordingClip = Audio.ListToClip(audioDataList);
        string filepath = Application.persistentDataPath + "/" + filename;
        int bitDepth = 16;
        int bitRate = frequency * bitDepth * channels;
        EncodeMP3.convert(recordingClip, filepath, bitRate);

        // TODO show toast save directory
        CancelAudio();
    }

    public void CancelAudio()
    {
        audioDataList.Clear();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
// using Firebase.Leaderboard;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Mode mode;

    [SerializeField]
    private GameObject listenPuzzleButton;

    public Operation operation;

    // holds reference to currently selected launcher or block, if any
    public Element currentElement;

    public bool isRecording;

    public bool isSharp;

    private List<float> audioDataList;

    private AudioClip recordingClip;

    [SerializeField]
    public GameObject guideDialog;

    [SerializeField]
    public GameObject saveDialog;
    private ToastCreator toastCreator;

    [SerializeField]
    private GameObject scoreDialog;
    private Text scoreText;

    // private LeaderboardController leaderboard;

    private void Start()
    {
        if (GuideController.GetGuidePref()) guideDialog.SetActive(true);
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
        toastCreator = GetComponent<ToastCreator>();
        // leaderboard = GetComponent<LeaderboardController>();
        isSharp = false;
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

    public void OnAudioFilterRead(float[] data, int channels)
    {
        if (isRecording && mode == Mode.FREE_PLAY)
            audioDataList.AddRange(data);
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
        toastCreator.CreateToast("Saved to " + filepath);
        CancelAudio();
    }

    public void CancelAudio()
    {
        audioDataList.Clear();
    }

    public void HandlePuzzleSubmission()
    {
        int score = Puzzle.selectedPuzzle.GetScore();
        Debug.Log(String.Format("Score: {0}", score));
        DisplayScore(score);
        // TODO update personal highscore (integrate with leaderboard?)
        // TODO upload to leaderboard
    }

    private void DisplayScore(int score)
    {
        scoreDialog.SetActive(true);
        if (scoreText == null)
            scoreText = scoreDialog.GetComponentInChildren<Text>();
        scoreText.text = String.Format("Score: {0}", score);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

    [SerializeField]
    public GameObject garbageObject;

    public bool isRecording;

    public bool isSharp;

    private List<float> audioDataList;

    private AudioClip recordingClip;

    [SerializeField]
    private GameObject saveDialog;
    private ToastCreator toastCreator;

    [SerializeField]
    private GameObject scoreDialog;
    private Text scoreText;
    private bool showNewHighScore;
    private bool savePlayerPrefHighScore;
    private int score;

    [SerializeField]
    private GameObject exitDialog;

    private void Start()
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
        toastCreator = GetComponent<ToastCreator>();
        isSharp = false;
        BackgroundMusic.Instance.StopMusic();
    }

    private void Update()
    {
        if (showNewHighScore)
        {
            showNewHighScore = false;
            toastCreator.CreateToast("New Highscore!");
        }
        if (savePlayerPrefHighScore)
        {
            savePlayerPrefHighScore = false;
            string key = Puzzle.selectedPuzzle.title + AccountManager.GetUserId();
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();
        }
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
        Puzzle puzzle = Puzzle.selectedPuzzle;
        score = puzzle.GetScore();
        Debug.Log(String.Format("Score: {0}", score));
        DisplayScore();
        IsNewHighScore().ContinueWith(task =>
        {
            bool isNewHighScore = task.Result;
            showNewHighScore = isNewHighScore;
            if (isNewHighScore)
            {
                savePlayerPrefHighScore = true;
                if (AccountManager.SignedIn())
                    AccountManager.UpdateHighScore(puzzle.title, score);
            }
        });
    }

    private void DisplayScore()
    {
        scoreDialog.SetActive(true);
        if (scoreText == null)
            scoreText = scoreDialog.GetComponentInChildren<Text>();
        scoreText.text = String.Format("Score: {0}", score);
    }

    private async Task<bool> IsNewHighScore()
    {
        Debug.Log("GameManager::IsNewHighScore");
        string puzzle = Puzzle.selectedPuzzle.title;
        long? highScore = null;
        // check PlayerPrefs first to minimize query from Firebase
        string key = puzzle + AccountManager.GetUserId();
        if (PlayerPrefs.HasKey(key)) highScore = PlayerPrefs.GetInt(key);
        else if (AccountManager.SignedIn())
        {
            await AccountManager.GetUserHighScore(puzzle).ContinueWith(task =>
            {
                if (task.IsFaulted) Debug.LogErrorFormat(
                    "GetUserHighScore encountered an error: {0}", task.Exception);
                else if (task.IsCompleted) highScore = task.Result;
            });
        }
        else Debug.Log("No locally saved high score. Not signed in");
        // highScore==null => no high score yet => is new high score
        return highScore == null || score > highScore;
    }

    public bool HaveDialogInFront()
    {
        return saveDialog.activeSelf ||
            scoreDialog.activeSelf || exitDialog.activeSelf;
    }
}

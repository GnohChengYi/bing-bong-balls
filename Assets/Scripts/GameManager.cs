using System.Collections;
using System.Collections.Generic;
using System.IO;
using Firebase.Leaderboard;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Mode
    {
        CREATE_LAUNCHER,
        CREATE_BLOCK,
        MOVE
    }

    public Mode mode;

    public bool isRecording;

    private AudioRenderer audioRenderer;

    private LeaderboardController leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        mode = Mode.MOVE;
        isRecording = false;
        leaderboard = GetComponent<LeaderboardController>();
        audioRenderer = new AudioRenderer();
        audioRenderer.rendering = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (isRecording) audioRenderer.OnAudioFilterRead(data, channels);
    }

    // TODO handle custom directory (including filename)
    public void SaveAudio()
    {
        string filepath = Application.persistentDataPath + "/temp_music.wav";
        audioRenderer.Save (filepath);
        audioRenderer = new AudioRenderer();
    }
}

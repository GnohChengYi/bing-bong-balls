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
        MOVE,
        RECORDING
    }

    public Mode mode;

    private AudioRenderer audioRenderer;

    private LeaderboardController leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        mode = Mode.MOVE;
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
        audioRenderer.Save("temp_music.wav");
    }

    public void OnAudioFilterRead(float[] data, int channels)
    {
        if (mode == Mode.RECORDING)
            audioRenderer.OnAudioFilterRead(data, channels);
    }
}

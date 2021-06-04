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

    private LeaderboardController leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        mode = Mode.SELECT;
        leaderboard = GetComponent<LeaderboardController>();
        audioRenderer = new AudioRenderer();
        audioRenderer.rendering = true;
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
        audioRenderer.Clear();
    }
}

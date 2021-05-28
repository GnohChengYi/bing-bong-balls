using System.Collections;
using System.Collections.Generic;
using System.IO;
using Firebase.Leaderboard;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject wallPrefab;

    private AudioRenderer audioRenderer;

    private LeaderboardController leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        leaderboard = GetComponent<LeaderboardController>();
        audioRenderer = new AudioRenderer();
        audioRenderer.rendering = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Debug.Log("Instatiate wallPrefab");
                Vector2 touchPos =
                    Camera.main.ScreenToWorldPoint(touch.position);
                Instantiate(wallPrefab, touchPos, Quaternion.identity);
            }
        }
    }

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy (Instance);
        }
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
        audioRenderer.Save("temp_music.wav");
    }

    // write the incoming audio to the output string
    public void OnAudioFilterRead(float[] data, int channels)
    {
        audioRenderer.OnAudioFilterRead (data, channels);
    }
}

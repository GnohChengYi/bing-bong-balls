using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;

    private bool shouldPlay;

    public static BackgroundMusic Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
        Instance.PlayMusic();
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        shouldPlay = true;
    }

    private void Update()
    {
        if (shouldPlay && !audioSource.isPlaying)
        {
            audioSource.clip = getRandomClip();
            audioSource.Play();
        }
        else if (!shouldPlay) audioSource.Stop();
    }

    public void PlayMusic()
    {
        shouldPlay = true;
    }

    public void StopMusic()
    {
        shouldPlay = false;
    }

    private AudioClip getRandomClip()
    {
        int index = Random.Range(0, audioClips.Length);
        return audioClips[index];
    }
}

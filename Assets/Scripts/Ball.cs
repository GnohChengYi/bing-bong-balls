using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameObject.name = Audio.lastSelectedInstrument;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Block")
        {
            string instrument = gameObject.name;
            string note = other.name;
            AudioClip audioClip = Audio.GetClip(instrument, note);
            audioSource.clip = audioClip;
            audioSource.Play();
            if (GameManager.Instance.mode == Mode.PUZZLE &&
                GameManager.Instance.isRecording)
                Puzzle.submission.Add(note);
        }
    }

    private void OnBecameInvisible()
    {
        // TODO check all balls leave screen before wrapping up
        UpdateHighScore();
        Destroy(gameObject);
    }

    private void UpdateHighScore()
    {
        // TODO send high score to Firebase
    }
}

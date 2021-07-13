using System.Collections;
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
            CreateRipples(collision);
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
        Destroy(gameObject);
    }

    private void CreateRipples(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
            RippleManager.CreateRippleAt(contact);
    }
}

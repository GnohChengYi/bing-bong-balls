using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class Soundable : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bong");
        audioSource.Play(0);
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        GameManager.Instance.OnAudioFilterRead (data, channels);
    }
}

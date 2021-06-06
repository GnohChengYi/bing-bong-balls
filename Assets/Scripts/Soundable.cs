using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class Soundable : MonoBehaviour
{
    private AudioSource audioSource;

    private float[] samples;

    private int channels;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioClip audioClip = audioSource.clip;
        samples = new float[audioClip.samples * audioClip.channels];
        audioClip.GetData(samples, 0);
        channels = audioClip.channels;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
    }
}

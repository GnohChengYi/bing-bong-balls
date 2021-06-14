using UnityEngine;

public class ListenPuzzleButton : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        Puzzle puzzle = Puzzle.selectedPuzzle;
        if (puzzle != null)
        {
            audioSource.clip = puzzle.GetAudioClip();
            audioSource.Play();
        }
    }
}

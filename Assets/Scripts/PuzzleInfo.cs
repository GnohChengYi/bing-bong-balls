using System;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleInfo : MonoBehaviour
{
    [SerializeField]
    private Text textComponent;

    private void Start()
    {
        if (GameManager.Instance.mode == Mode.PUZZLE)
        {
            gameObject.SetActive(true);
            Puzzle puzzle = Puzzle.selectedPuzzle;
            int maxScore = puzzle.notes.Count;
            string text =
                String.Format("Puzzle: {0}\nMax Score: {1}", puzzle.title, maxScore);
            textComponent.text = text;
        }
        else gameObject.SetActive(false);
    }
}
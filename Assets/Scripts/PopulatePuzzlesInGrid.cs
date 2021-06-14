using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulatePuzzlesInGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject puzzlePrefab;

    private void Start()
    {
        Puzzle.Init();
        foreach (Puzzle puzzle in Puzzle.puzzles)
        {
            GameObject puzzleObject =
                (GameObject)Instantiate(puzzlePrefab, transform);
            Text text = puzzleObject.GetComponentInChildren<Text>();
            text.text = puzzle.title;
            Button button = puzzleObject.GetComponent<Button>();
            button.onClick.AddListener(
                () => Puzzle.selectedPuzzle = puzzle);
        }
    }
}
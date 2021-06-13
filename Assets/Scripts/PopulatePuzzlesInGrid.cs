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
        Puzzle.InitPuzzles();
        foreach (Puzzle puzzle in Puzzle.puzzles)
        {
            GameObject puzzleObject = (GameObject)Instantiate(puzzlePrefab, transform);
            puzzleObject.GetComponentInChildren<Text>().text = puzzle.title;
        }
    }
}

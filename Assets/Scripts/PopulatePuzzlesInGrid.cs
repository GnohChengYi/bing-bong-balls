using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            puzzleObject.GetComponentInChildren<Text>().text = puzzle.title;
            Button button = puzzleObject.GetComponent<Button>();
            button.onClick.AddListener(() => Puzzle.selectedPuzzle = puzzle);
            Transform scoreTransform = puzzleObject.transform.Find("Container/ScoreText");
            scoreTransform.GetComponent<Text>().text = GetScoreString(puzzle);
        }
    }

    private string GetScoreString(Puzzle puzzle)
    {
        string key = puzzle.title + AccountManager.GetUserId();
        string highScoreString = "-";
        if (PlayerPrefs.HasKey(key))
            highScoreString = PlayerPrefs.GetInt(key).ToString();
        string maxScoreString = puzzle.notes.Count.ToString();
        return highScoreString + "/" + maxScoreString;
    }
}
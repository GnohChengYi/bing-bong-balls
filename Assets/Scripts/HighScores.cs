using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    [SerializeField]
    private Dropdown dropdown;
    [SerializeField]
    private GameObject highScorePrefab;
    [SerializeField]
    private Transform contentTransform;

    private List<UserScore> highScores = new List<UserScore>();
    private bool reloadHighScores;

    private void Start()
    {
        if (Puzzle.puzzles == null) Puzzle.Init();
        // TODO populate dropdown with valid puzzles
        List<string> options = GetOptions();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
        ReloadHighScores();
    }

    private void Update()
    {
        if (reloadHighScores)
        {
            reloadHighScores = false;
            int i = 0;
            // reuse existing highScoreGameObjects
            for (; i < contentTransform.childCount && i < highScores.Count; i++)
            {
                Transform highScoreTransform = contentTransform.GetChild(i);
                UpdateText(highScoreTransform, highScores[i]);
                highScoreTransform.gameObject.SetActive(true);
            }
            // deactivate extra highScoreGameObjects
            for (; i < contentTransform.childCount; i++)
            {
                Transform highScoreTransform = contentTransform.GetChild(i);
                highScoreTransform.gameObject.SetActive(false);
            }
            // create more highScoreGameObjects if needed
            for (; i < highScores.Count; i++)
            {
                GameObject highScoreObject = Instantiate(highScorePrefab) as GameObject;
                Transform highScoreTransform = highScoreObject.transform;
                highScoreTransform.SetParent(contentTransform, false);
                UpdateText(highScoreTransform, highScores[i]);
            }
        }
    }

    public void ReloadHighScores()
    {
        Debug.Log("DisplayHighScores");
        string puzzle = dropdown.captionText.text;
        Debug.LogFormat("Puzzle: {0}", puzzle);
        GlobalHighScoresManager.GetHighScores(puzzle).ContinueWith(task =>
        {
            if (task.IsFaulted) Debug.LogErrorFormat(
                "GetHighScores encountered an error: {0}", task.Exception);
            else if (task.IsCompleted)
            {
                highScores = task.Result;
                reloadHighScores = true;
            }
        });
    }

    private List<string> GetOptions()
    {
        return Enumerable.Range(1, Puzzle.puzzles.Count)
            .Select(i => i.ToString()).ToList();
    }

    private void UpdateText(Transform highScoreTransform, UserScore highScore)
    {
        Text nameText = highScoreTransform.GetChild(0).GetComponent<Text>();
        Text scoreText = highScoreTransform.GetChild(1).GetComponent<Text>();
        nameText.text = highScore.userId;
        scoreText.text = highScore.score.ToString();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle
{
    public static List<Puzzle> puzzles;

    public static Puzzle selectedPuzzle;

    public readonly string title;

    // TODO create getter
    public List<string> notes;

    public Puzzle(string commaSeparatedTitleAndNotes)
    {
        string[] split = commaSeparatedTitleAndNotes.Split(',');
        title = split[0];
        notes = new List<string>(split).GetRange(1, split.Length - 1);
    }

    public AudioClip GetAudioClip()
    {
        List<float> audioDataList = new List<float>();
        foreach (string note in notes)
        {
            AudioClip audioClip = Audio.GetClip(Audio.instruments[0], note);
            float[] samples = new float[audioClip.samples * audioClip.channels];
            audioClip.GetData(samples, 0);
            audioDataList.AddRange(samples);
            float[] muteSamples = new float[samples.Length / 2];
            audioDataList.AddRange(muteSamples);
        }
        return Audio.ListToClip(audioDataList);
    }

    public int GetScore(List<string> submission)
    {
        return notes.Count - EditDistanceBetweenNotesAnd(submission);
    }

    // Wagner-Fischer algorithm
    private int EditDistanceBetweenNotesAnd(List<string> submission)
    {
        Debug.Log("notes: " + string.Join(", ", notes.ToArray()));
        int[,] distances = new int[notes.Count + 1, submission.Count + 1];
        for (int i = 1; i <= notes.Count; i++) distances[i, 0] = i;
        for (int j = 1; j <= submission.Count; j++) distances[0, j] = j;
        for (int j = 1; j <= submission.Count; j++)
            for (int i = 1; i <= notes.Count; i++)
            {
                int substitutionCost = 0;
                if (!String.Equals(notes[i - 1], submission[j - 1], StringComparison.InvariantCulture))
                    substitutionCost = 1;
                distances[i, j] = Min(distances[i - 1, j] + 1,
                    distances[i, j - 1] + 1,
                    distances[i - 1, j - 1] + substitutionCost);
            }
        return distances[notes.Count, submission.Count];
    }

    private int Min(int value1, int value2, int value3)
    {
        if (value1 <= value2 && value1 <= value3)
            return value1;
        else if (value2 <= value1 && value2 <= value3)
            return value2;
        else
            return value3;
    }

    // File Format:
    // Each line represents one puzzle
    // Comma separated values, first entry is title, remaining entries are notes
    // e.g.
    // 1,a3,a4,a-3
    public static void Init()
    {
        selectedPuzzle = null;
        puzzles = new List<Puzzle>();
        // TODO try use [SerializeField] for TextAsset for .txt
        string rawText = Resources.Load<TextAsset>("puzzles").text;
        string[] lines = rawText.Split('\n');
        foreach (string line in lines)
        {
            Puzzle puzzle = new Puzzle(line);
            puzzles.Add(puzzle);
        }
    }
}

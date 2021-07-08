using Firebase.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GlobalHighScoresManager : MonoBehaviour
{
    // TODO change limit to 100 in release
    public const int limit = 10;

    public static async Task<List<UserScore>> GetHighScores(string puzzle)
    {
        string path = GetPuzzlePath(puzzle);
        Query query = AccountManager.database.GetReference(path)
            .OrderByValue().LimitToLast(limit);
        List<UserScore> highScores = new List<UserScore>();
        await query.GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = null;
            if (task.IsFaulted) Debug.LogErrorFormat(
                "GetValueAsync encountered an error: {0}", task.Exception);
            else if (task.IsCompleted) snapshot = task.Result;
            return snapshot;
        }).ContinueWith(task => GetHighScoresFromSnapshot(task.Result)).Unwrap()
            .ContinueWith(task => highScores = task.Result);
        return highScores;
    }

    public static void AddHighScore(string puzzle, string userId, int score)
    {
        string path = GetPuzzlePath(puzzle) + "/" + userId;
        AccountManager.database.GetReference(path).SetValueAsync(score)
            .ContinueWith(Task => Debug.Log("Submitted score to global high scores"));
    }

    private static async Task<List<UserScore>> GetHighScoresFromSnapshot(DataSnapshot snapshot)
    {
        List<UserScore> result = new List<UserScore>();
        foreach (DataSnapshot record in snapshot.Children)
            await UserScore.CreateScoreFromRecord(record).ContinueWith(
                task => result.Add(task.Result));
        result.Reverse();
        return result;
    }

    private static string GetPuzzlePath(string puzzle)
    {
        return "high-scores/" + puzzle;
    }
}

using Firebase.Auth;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class UserScore
{
    public string userId;
    public string displayName;
    public int score;

    private UserScore(string userId, string displayName, int score)
    {
        this.userId = userId;
        this.displayName = displayName;
        this.score = score;
    }

    public static async Task<UserScore> CreateScoreFromRecord(
        DataSnapshot record)
    {
        string userId = record.Key;
        int score = (int)((long)record.Value);
        UserScore userScore = null;
        await AccountManager.GetDisplayNameByUserId(userId).ContinueWith(task =>
        {
            string displayName = task.Result;
            userScore = new UserScore(userId, displayName, score);
        });
        return userScore;
    }

    public override string ToString()
    {
        return String.Format("{0} ({1}) : {2}", displayName, userId, score);
    }
}
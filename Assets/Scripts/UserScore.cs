using Firebase.Database;
using UnityEngine;

public class UserScore
{
    public string userId;
    public int score;

    private UserScore(string userId, int score)
    {
        this.userId = userId;
        this.score = score;
    }

    public static UserScore CreateScoreFromRecord(DataSnapshot record)
    {
        string userId = record.Key;
        int score = (int)((long)record.Value);
        return new UserScore(userId, score);
    }
}
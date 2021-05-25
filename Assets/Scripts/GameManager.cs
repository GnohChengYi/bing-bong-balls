using Firebase.Leaderboard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LeaderboardController leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        leaderboard = GetComponent<LeaderboardController>();
        leaderboard.AddScore("userIDDDD", "usernammeee", 12344);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

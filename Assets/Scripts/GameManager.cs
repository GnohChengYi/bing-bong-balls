using System.Collections;
using System.Collections.Generic;
using Firebase.Leaderboard;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject wallPrefab;

    private LeaderboardController leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        leaderboard = GetComponent<LeaderboardController>();

        // TODO remove before production
        leaderboard.AddScore("userIDDDD", "usernammeee", 12344);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Instatiate wallPrefab");
                Vector2 touchPos =
                    Camera.main.ScreenToWorldPoint(touch.position);
                Instantiate(wallPrefab, touchPos, Quaternion.identity);
            }
        }
    }
}

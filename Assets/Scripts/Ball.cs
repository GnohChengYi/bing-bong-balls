using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D ballRigidbody;

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        // TODO check all balls leave screen before wrapping up
        UpdateHighScore();

        // TODO make sure actually destory ball by checking scene
        Destroy (gameObject);
    }

    private void UpdateHighScore()
    {
        // TODO send high score to Firebase
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private float speed = 3.0F;

    [SerializeField]
    private GameObject ballPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        Launch();
    }

    private void Launch()
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.position = transform.position + transform.up;
        ball.transform.rotation = transform.rotation;
        ball.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }
}

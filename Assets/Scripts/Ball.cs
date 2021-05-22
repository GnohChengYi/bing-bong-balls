using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D ballRigidbody;
    private Vector2 lastVelocity;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        // TODO let user customize velocity
        ballRigidbody.velocity = new Vector2(1f, 1f) * speed;
    }

    // FixedUpdate: for regular updates such as: Adjusting Physics objects
    private void FixedUpdate()
    {
        lastVelocity = ballRigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // velocity changed at collision, so need use last velocity
        Vector2 inDirection = lastVelocity;
        Vector2 inNormal = collision.GetContact(0).normal;
        ballRigidbody.velocity = Vector2.Reflect(inDirection, inNormal) * speed;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private Collider2D collider;

    private float deltaX;

    private float deltaY;

    private bool moveAllowed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            Input.touchCount > 0 &&
            GameManager.Instance.mode == GameManager.Mode.MOVE
        )
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (collider == Physics2D.OverlapPoint(touchPos))
                    {
                        // diff btw touch position & center GameObject
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;
                        rigidbody.constraints =
                            RigidbodyConstraints2D.FreezeRotation;
                        moveAllowed = true;
                    }
                    break;
                case TouchPhase.Moved:
                    if (
                        collider == Physics2D.OverlapPoint(touchPos) &&
                        moveAllowed
                    )
                    {
                        float x = (float) Math.Round(touchPos.x - deltaX);
                        float y = (float) Math.Round(touchPos.y - deltaY);
                        rigidbody.MovePosition(new Vector2(x, y));
                    }
                    break;
                case TouchPhase.Ended:
                    rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                    moveAllowed = false;
                    break;
            }
        }
    }
}

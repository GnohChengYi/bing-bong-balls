// based on https://www.patrykgalach.com/2019/05/09/drag-and-drop-in-unity/?cn-reloaded=1
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class
Draggable
: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;

    private GraphicRaycaster graphicRaycaster;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        // there is canvas scaler, so need divide pointer delta by canbas scale to match pointer movement
        transform.localPosition +=
            new Vector3(eventData.delta.x, eventData.delta.y, 0) /
            transform.lossyScale.x;
        if (!canvas)
        {
            canvas = GetComponentInParent<Canvas>();
            graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        }
        transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // there is canvas scaler, so need divide pointer delta by canbas scale to match pointer movement
        transform.position +=
            new Vector3(eventData.delta.x, eventData.delta.y, 0) /
            transform.lossyScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }

    // private Rigidbody2D rigidbody;

    // private Collider2D collider;

    // private float deltaX;

    // private float deltaY;

    // private bool moveAllowed;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     rigidbody = GetComponent<Rigidbody2D>();
    //     collider = GetComponent<Collider2D>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (
    //         Input.touchCount > 0 &&
    //         GameManager.Instance.mode == GameManager.Mode.MOVE
    //     )
    //     {
    //         Touch touch = Input.GetTouch(0);
    //         Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
    //         switch (touch.phase)
    //         {
    //             case TouchPhase.Began:
    //                 if (collider == Physics2D.OverlapPoint(touchPos))
    //                 {
    //                     // diff btw touch position & center GameObject
    //                     deltaX = touchPos.x - transform.position.x;
    //                     deltaY = touchPos.y - transform.position.y;
    //                     rigidbody.constraints =
    //                         RigidbodyConstraints2D.FreezeRotation;
    //                     moveAllowed = true;
    //                 }
    //                 break;
    //             case TouchPhase.Moved:
    //                 if (
    //                     collider == Physics2D.OverlapPoint(touchPos) &&
    //                     moveAllowed
    //                 )
    //                 {
    //                     float x = (float) Math.Round(touchPos.x - deltaX);
    //                     float y = (float) Math.Round(touchPos.y - deltaY);
    //                     rigidbody.MovePosition(new Vector2(x, y));
    //                 }
    //                 break;
    //             case TouchPhase.Ended:
    //                 rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    //                 moveAllowed = false;
    //                 break;
    //         }
    //     }
    // }
}

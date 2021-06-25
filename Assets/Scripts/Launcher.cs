using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Launcher : MonoBehaviour, ISelectHandler,
    IDeselectHandler, IBeginDragHandler, Element
{
    [SerializeField]
    private GameObject ballPrefab;

    private float speed = 3.0F;

    public string instrument;

    private Selectable selectable;

    public bool isActive = true;

    private void Start()
    {
        selectable = GetComponent<Selectable>();
        instrument = Audio.lastSelectedInstrument;
    }

    public void Select()
    {
        selectable.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameManager.Instance.currentElement = this;
        isActive = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isActive = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isActive = false;
    }

    public void FacePointer()
    {
        Vector3 selfPosition =
            Camera.main.WorldToScreenPoint(transform.position);
        Vector3 upwards = Input.mousePosition - selfPosition;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, upwards);
    }

    public void Launch()
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.position = transform.position + transform.up;
        ball.transform.rotation = transform.rotation;
        ball.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        ball.name = instrument;
    }
}

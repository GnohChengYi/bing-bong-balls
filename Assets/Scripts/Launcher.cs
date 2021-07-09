using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Launcher : MonoBehaviour, ISelectHandler,
    IDeselectHandler, IPointerUpHandler, Element
{
    [SerializeField]
    private GameObject ballPrefab;

    private float speed = 3.0F;

    private Selectable selectable;

    public bool isActive = true;

    private void Start()
    {
        selectable = GetComponent<Selectable>();
    }

    public void Select()
    {
        selectable.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameManager.Instance.currentElement = this;
        Audio.lastSelectedInstrument = name;
        isActive = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isActive = false;
    }

    public void OnPointerUp(PointerEventData eventData)
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
        string ballPath = "Balls/" + Audio.lastSelectedInstrument;
        ball.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(ballPath);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Launcher : MonoBehaviour, ISelectHandler, IDeselectHandler, IBeginDragHandler
{
    private float speed = 3.0F;

    [SerializeField]
    private GameObject ballPrefab;

    private Selectable selectable;

    public bool shouldLaunch;

    // Start is called before the first frame update
    private void Start()
    {
        selectable = GetComponent<Selectable>();
        selectable.Select();
        GameManager.Instance.lastSelected = selectable;
        GameManager.Instance.launcher = this;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
            FacePointer();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameManager.Instance.lastSelected = selectable;
        GameManager.Instance.launcher = this;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        shouldLaunch = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        shouldLaunch = false;
    }

    private void FacePointer()
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Launcher : MonoBehaviour, ISelectHandler
{
    private float speed = 3.0F;

    [SerializeField]
    private GameObject ballPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        Launch();
    }

    private void Update()
    {
        FaceTouch();
    }

    public static Launcher Instance { get; private set; }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Launcher selected");
        Instance = this;
    }

    private void FaceTouch()
    {
        Vector3 selfPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 upwards = Input.mousePosition - selfPosition;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, upwards);
    }

    private void Launch()
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.position = transform.position + transform.up;
        ball.transform.rotation = transform.rotation;
        ball.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }
}

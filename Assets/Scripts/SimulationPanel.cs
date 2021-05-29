using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject launcher;

    [SerializeField]
    private GameObject block;

    private RectTransform rect;

    // Start is called before the first frame update
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            if (
                touch.phase == TouchPhase.Began &&
                RectTransformUtility
                    .RectangleContainsScreenPoint(rect, touch.position, null)
            )
            {
                Vector2 touchPos =
                    Camera.main.ScreenToWorldPoint(touch.position);
                if (
                    GameManager.Instance.mode ==
                    GameManager.Mode.CREATE_LAUNCHER
                )
                    Instantiate(launcher, touchPos, Quaternion.identity);
                else if (
                    GameManager.Instance.mode == GameManager.Mode.CREATE_BLOCK
                ) Instantiate(block, touchPos, Quaternion.identity);
            }
        }
    }
}

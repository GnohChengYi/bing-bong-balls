using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationPanel : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject launcher;

    [SerializeField]
    private GameObject block;

    private RectTransform rectTransform;

    private float scale;

    // Start is called before the first frame update
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        scale = 0;
    }

    private void initScale()
    {
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        scale = Math.Min(width, height) / 8F;
        Debug.Log (scale);
    }

    // Update is called once per frame
    private void Update()
    {
        if (scale == 0)
        {
            initScale();
        }
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            if (
                touch.phase == TouchPhase.Began &&
                RectTransformUtility
                    .RectangleContainsScreenPoint(rectTransform,
                    touch.position,
                    camera)
            )
            {
                Vector2 touchPos =
                    Camera.main.ScreenToWorldPoint(touch.position);
                if (
                    GameManager.Instance.mode ==
                    GameManager.Mode.CREATE_LAUNCHER
                )
                {
                    GameObject newLauncher =
                        Instantiate(launcher,
                        touchPos,
                        Quaternion.identity,
                        transform);
                    newLauncher.transform.localScale *= scale;
                }
                else if (
                    GameManager.Instance.mode == GameManager.Mode.CREATE_BLOCK
                )
                {
                    GameObject newBlock =
                        Instantiate(block,
                        touchPos,
                        Quaternion.identity,
                        transform);
                    newBlock.transform.localScale *= scale;
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimulationPanel : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject launcher;

    [SerializeField]
    private GameObject block;

    private RectTransform rectTransform;

    public float scale;

    // Start is called before the first frame update
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        scale = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (scale == 0) InitScale();
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            if (!InPanel(touch)) return;
            if (touch.phase == TouchPhase.Began)
            {
                // auto deselect when click on Panel, so need to select back last selected Launcher or Block
                Selectable lastSelected = GameManager.Instance.lastSelected;
                if (lastSelected) lastSelected.Select();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (
                    GameManager.Instance.mode ==
                    GameManager.Mode.CREATE_LAUNCHER
                )
                    CreateAt(launcher, touch);
                else if (
                    GameManager.Instance.mode == GameManager.Mode.CREATE_BLOCK
                )
                    CreateAt(block, touch);
                else if (GameManager.Instance.mode == GameManager.Mode.SELECT)
                {
                    Launcher launcher = GameManager.Instance.launcher;
                    if (!launcher) return;
                    if (launcher.shouldLaunch)
                        launcher.Launch();
                    else
                        launcher.shouldLaunch = true;
                }
            }
        }
    }

    private void InitScale()
    {
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        scale = Math.Min(width, height) / 8F;
    }

    private bool InPanel(Touch touch)
    {
        return RectTransformUtility
            .RectangleContainsScreenPoint(rectTransform,
            touch.position,
            camera);
    }

    // TODO create at same z as panel
    private void CreateAt(GameObject gameObject, Touch touch)
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        GameObject newGameObject =
            Instantiate(gameObject, touchPos, Quaternion.identity, transform);
        newGameObject.transform.localScale *= scale;
        GameManager.Instance.mode = GameManager.Mode.SELECT;
    }
}

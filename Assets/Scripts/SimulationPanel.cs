using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimulationPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject launcherPrefab;

    [SerializeField]
    private GameObject blockPrefab;

    private RectTransform rectTransform;

    public float scale;

    private ToastCreator toastCreator;

    // Start is called before the first frame update
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        scale = 0;
        toastCreator = GetComponent<ToastCreator>();
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
                // auto deselect when click on Panel, so need to select back last selected Launcher
                Element currentElement =
                    GameManager.Instance.currentElement;
                if (currentElement != null && currentElement is Launcher)
                    currentElement.Select();
                if (GameManager.Instance.operation == Operation.SELECT &&
                    !GameManager.Instance.HaveDialogInFront() &&
                    currentElement == null)
                    AlertInvalidOperation();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (GameManager.Instance.operation == Operation.CREATE_LAUNCHER)
                {
                    GameObject currLauncher = CreateAt(launcherPrefab, touch);
                    currLauncher.name = Audio.lastSelectedInstrument;
                    string launcherPath = "Launchers/" + currLauncher.name;
                    currLauncher.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(launcherPath);
                    GameManager.Instance.operation = Operation.SELECT;
                }
                else if (GameManager.Instance.operation == Operation.CREATE_BLOCK)
                {
                    GameObject block = CreateAt(blockPrefab, touch);
                    block.name = Audio.lastSelectedNote;
                    string imgPath = "Blocks/" + block.name;
                    block.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(imgPath);
                }
                else if (GameManager.Instance.operation == Operation.SELECT)
                {
                    Element currentElement = GameManager.Instance.currentElement;
                    if (currentElement is Launcher)
                    {
                        Launcher launcher = (Launcher)currentElement;
                        if (launcher.isActive)
                        {
                            launcher.FacePointer();
                            launcher.Launch();
                        }
                    }
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
            Camera.main);
    }

    // TODO create at same z as panel
    private GameObject CreateAt(GameObject gameObject, Touch touch)
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        GameObject newGameObject =
            Instantiate(gameObject, touchPos, Quaternion.identity, transform);
        newGameObject.transform.localScale *= scale;
        return newGameObject;
    }

    private void AlertInvalidOperation()
    {
        toastCreator.CreateToast(
            "Please select a launcher or choose something to create");
    }
}

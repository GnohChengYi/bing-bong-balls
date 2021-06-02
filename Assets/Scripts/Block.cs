using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Block : MonoBehaviour, ISelectHandler
{
    private Selectable selectable;

    private void Start()
    {
        selectable = GetComponent<Selectable>();
        selectable.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameManager.Instance.lastSelected = selectable;
        GameManager.Instance.launcher = null;
        // TODO change note
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Block :
    MonoBehaviour, ISelectHandler, IDeselectHandler, Element
{
    private Selectable selectable;

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
        // TODO change note
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GameManager.Instance.currentElement = null;
    }
}

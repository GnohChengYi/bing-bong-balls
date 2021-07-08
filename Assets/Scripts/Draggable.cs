// based on https://www.patrykgalach.com/2019/05/09/drag-and-drop-in-unity/?cn-reloaded=1
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class
Draggable
: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.localPosition += GetDeltaVector(eventData);
        transform.SetAsLastSibling();
        GameManager.Instance.garbageObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition += GetDeltaVector(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // TODO fix grid dimensions for all screen sizes
        // snap to grid: round position to nearest
        int nearest = 30;
        float snapX =
            (float)((int)transform.localPosition.x / nearest) * nearest;
        float snapY =
            (float)((int)transform.localPosition.y / nearest) * nearest;
        transform.localPosition = new Vector3(snapX, snapY, 0);
        GameManager.Instance.garbageObject.SetActive(false);
        if (ShouldDelete()) Destroy(gameObject);
    }

    public Vector3 GetDeltaVector(PointerEventData eventData)
    {
        float screenSize = Math.Max(Screen.width, Screen.height);
        float deltaX = eventData.delta.x * 1920 / screenSize;
        float deltaY = eventData.delta.y * 1920 / screenSize;
        return new Vector3(deltaX, deltaY, 0);
    }

    private bool ShouldDelete()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewPos.x > 0.8 && viewPos.y > 0.9;
    }
}

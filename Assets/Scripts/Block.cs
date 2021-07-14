using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Block :
    MonoBehaviour, ISelectHandler, IDeselectHandler, Element
{
    [SerializeField]
    private Selectable selectable;
    [SerializeField]
    private Image image;

    private Color dimColor;

    private void Start()
    {
        dimColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
    }

    public void Select()
    {
        selectable.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameManager.Instance.currentElement = this;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GameManager.Instance.currentElement = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DimAnimation());
    }

    private IEnumerator DimAnimation()
    {
        image.color = dimColor;
        yield return new WaitForSeconds(0.1f);
        image.color = Color.white;
    }
}

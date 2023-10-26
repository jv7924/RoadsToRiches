using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform parent;

    private Vector3 originalSize;

    private Vector3 shrinkSize;

    public float shrink;

    void Awake()
    {
        originalSize = this.transform.localScale;
        shrinkSize = originalSize * shrink;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        this.transform.localScale = shrinkSize;
        parent.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.localScale = originalSize;
        this.transform.SetParent(parent);
        parent.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GridManager gridManager;

    [SerializeField]
    private Color cardFace;

    private Transform parent;

    private Vector3 originalSize;

    private Vector3 shrinkSize;

    public float shrink;

    void Awake()
    {
        originalSize = transform.localScale;
        shrinkSize = originalSize * shrink;
        gridManager = FindObjectOfType<GridManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.localScale = shrinkSize;
        parent.gameObject.SetActive(false);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gridManager.addToList(hit.transform.name, gameObject);
            if(hit.transform.gameObject.CompareTag("Board"))
            {
                var renderer = hit.transform.gameObject.GetComponent<Renderer>();
                renderer.material.SetColor("_Color", cardFace);
            }
            Destroy(eventData.pointerDrag);
        }
        else
        {
            transform.localScale = originalSize;
            transform.SetParent(parent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        parent.gameObject.SetActive(true);
    }
}

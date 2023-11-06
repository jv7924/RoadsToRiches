using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GridManager gridManager;

    [SerializeField]
    private Road road;

    [SerializeField]
    private GameObject tilePrefab;

    private Transform parent;

    private Vector3 originalSize;

    private Vector3 shrinkSize;

    private int rotation;

    private bool isDrag;

    public float shrink;

    void Awake()
    {
        rotation = 0;
        originalSize = transform.localScale;
        shrinkSize = originalSize * shrink;
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        if(isDrag)
        {
            if(Input.GetKeyDown("e"))
            {
                transform.Rotate(0, 0, -90);
                rotation += 90;
                road.RotateClock();
            }
            else if(Input.GetKeyDown("q"))
            {
                transform.Rotate(0, 0, 90);
                rotation -= 90;
                road.RotateCounterClock();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.localScale = shrinkSize;
        parent.gameObject.SetActive(false);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        isDrag = true;
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
            gridManager.addToList(hit.transform.name, road);
            if(hit.transform.gameObject.CompareTag("Board"))
            {
                GameObject tile = Instantiate(tilePrefab, hit.transform.position, hit.transform.rotation);
                tile.transform.Rotate(0, rotation, 0);
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
        isDrag = false;
    }
}
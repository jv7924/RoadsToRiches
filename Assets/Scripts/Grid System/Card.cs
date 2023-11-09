using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Photon.Pun;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GridManager gridManager;

    [SerializeField]
    private OfflineTurnSystem offlineTurnSystem;

    [SerializeField]
    public Road road;

    [SerializeField]
    public GameObject tilePrefab;

    public string prefabName;

    private Transform parent;

    private Vector3 originalSize;

    private Vector3 shrinkSize;

    private int rotation;

    private bool isDrag;

    public float shrink;

    public PhotonView pv;

    void Awake()
    {
        rotation = 0;
        originalSize = transform.localScale;
        shrinkSize = originalSize * shrink;
        gridManager = FindObjectOfType<GridManager>();
        offlineTurnSystem = FindObjectOfType<OfflineTurnSystem>();
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
            if(hit.transform.gameObject.CompareTag("Board"))
            {
<<<<<<< HEAD
                GameObject tile = PhotonNetwork.Instantiate(prefabName, hit.transform.position, hit.transform.rotation);
=======
                gridManager.checkSurroundingCoords(hit.transform.name);
                gridManager.addToList(hit.transform.name, road);
                GameObject tile = Instantiate(tilePrefab, hit.transform.position + new Vector3(0, .05f, 0), hit.transform.rotation);
>>>>>>> 53bd4519da41590f3ea71b42a168d9002b63b524
                tile.transform.Rotate(0, rotation, 0);
                Destroy(eventData.pointerDrag);
                offlineTurnSystem.ChangeTurn();
            }            
            else
            {
                transform.localScale = originalSize;
                transform.SetParent(parent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
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

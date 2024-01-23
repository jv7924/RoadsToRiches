using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class BombCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GridManager gridManager;

    [SerializeField]
    private OfflineTurnSystem offlineTurnSystem;

    [SerializeField]
    public GameObject discardPile;

    private Transform parent;

    private Vector3 originalSize;

    private Vector3 shrinkSize;

    public float shrink;

    [SerializeField] private TMP_Text winnerText;
    private GameObject player1Canvas;
    private GameObject player2Canvas;
    private GameObject winCanvas;
    private GameObject chipsCanvas;

    void Awake()
    {
        originalSize = transform.localScale * 2;
        shrinkSize = originalSize * shrink;
        gridManager = FindObjectOfType<GridManager>();
        offlineTurnSystem = FindObjectOfType<OfflineTurnSystem>();
        discardPile = GameObject.FindWithTag("Discard Pile");

        //Win Screen
        //tempText = GameObject.Find("WinnerText");
        //winnerText = tempText.GetComponent<TextMeshProUGUI>();
        player1Canvas = GameObject.Find("Player 1 Canvas");
        player2Canvas = GameObject.Find("Player 2 Canvas");
        winCanvas = GameObject.Find("WinCanvas");
        chipsCanvas = GameObject.Find("PokerChipsCanvas");
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
        RaycastHit hitRoad;
        RaycastHit hitTile;
        if (Physics.Raycast(ray, out hitRoad, 1000))
        {
            if(hitRoad.transform.gameObject.CompareTag("Road"))
            {
                int layerMask = 1 << 7;
                if (Physics.Raycast(ray, out hitTile, 1000, layerMask))
                {
                    Destroy(hitRoad.transform.gameObject);
                    gridManager.DeleteFromList(hitTile.transform.gameObject);
                    transform.SetParent(discardPile.transform);
                    //OnlineTurnSystem.instance.photonView.RPC("RPC_IncrementTurn", RpcTarget.AllBuffered);
                    offlineTurnSystem.ChangeTurn();
                    gridManager.PlayDrawSound();
                }
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
    }
}

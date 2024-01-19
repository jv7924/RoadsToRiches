using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GridManager gridManager;

    [SerializeField]
    private OfflineTurnSystem offlineTurnSystem;

    [SerializeField]
    public GameObject discardPile;

    [SerializeField]
    public Road road;

    [SerializeField]
    public GameObject tilePrefab;

    private Transform parent;

    private Vector3 originalSize;

    private Vector3 shrinkSize;

    private int rotation;

    private bool isDrag;

    public float shrink;

    [SerializeField] private TMP_Text winnerText;
    private GameObject player1Canvas;
    private GameObject player2Canvas;
    private GameObject winCanvas;
    private GameObject chipsCanvas;

    void Awake()
    {
        rotation = 0;
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
                

                if(gridManager.checkSurroundingCoords(hit.transform.name, road)) //Check for valid road placement
                {
                    gridManager.PlayBuildSound();
                    GameObject tile = PhotonNetwork.Instantiate(tilePrefab.name, hit.transform.position + new Vector3(0, .05f, 0), hit.transform.rotation);
                    // GameObject tile = Instantiate(tilePrefab, hit.transform.position + new Vector3(0, .05f, 0), hit.transform.rotation); // offline
                    
                    // gameObject.GetPhotonView().RPC("RPC_InstantiateRoad", RpcTarget.Others, hit.transform.position, hit.transform.rotation);

                    eventData.pointerDrag.transform.SetParent(discardPile.transform);
                    // gridManager.addToList(hit.transform.name, road);    // offline
                    gridManager.photonView.RPC("RPC_addToList", RpcTarget.All, hit.transform.name, road.name, road.up.Key, road.down.Key, road.left.Key, road.right.Key, rotation);
                    tile.transform.Rotate(0, rotation, 0);
                    // offlineTurnSystem.ChangeTurn(); // offline
                    OnlineTurnSystem.instance.photonView.RPC("RPC_IncrementTurn", RpcTarget.AllBuffered);
                    gridManager.PlayDrawSound();
                    // Destroy(eventData.pointerDrag);
                }
                else 
                {
                    transform.SetParent(parent);
                    transform.localScale = originalSize;
                    GetComponent<CanvasGroup>().blocksRaycasts = true;
                }

                /*gridManager.checkSurroundingCoords(hit.transform.name, road);
                gridManager.PlayBuildSound();
                gridManager.addToList(hit.transform.name, road);
                //gridManager.checkRoad(road);
                GameObject tile = Instantiate(tilePrefab, hit.transform.position + new Vector3(0, .05f, 0), hit.transform.rotation);
                tile.transform.Rotate(0, rotation, 0);
                //Destroy(eventData.pointerDrag);
                eventData.pointerDrag.transform.parent = discardPile.transform;
                offlineTurnSystem.ChangeTurn();
                gridManager.PlayDrawSound();*/
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

        // Temporary debug for checking if the game is won
        int returnValue = gridManager.CheckIfWon();
        if (returnValue != 0)
        {
            //SceneManager.LoadScene("PlayerWin");
            // Debug.Log(winCanvas);
            // Debug.Log($"Player {returnValue} has won!");
            // player1Canvas.SetActive(false);
            // player2Canvas.SetActive(false);
            // winCanvas.SetActive(true);
            // chipsCanvas.SetActive(true);
            // winCanvas.GetComponent<WinCanvas>().UpdateText(returnValue);
            gridManager.GameWon(returnValue);
        }
    }
}

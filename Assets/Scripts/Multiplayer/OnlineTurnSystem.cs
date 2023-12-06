using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public class OnlineTurnSystem : MonoBehaviour
{
    public static OnlineTurnSystem instance;
    public PhotonView photonView;
    public GameObject localPlayer;
    public GameObject otherPlayer;
    public List<GameObject> allPlayers;
    public int[] probabilityBins = new int[5];
    
    public GameObject roadBlock, fourWay, tIntersection, bendedTurn, straight;

    private int turn;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(instance.gameObject);

                instance = this;
            }
        }

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        turn = 0;

        photonView = GetComponent<PhotonView>();

        Invoke("StartingDraw", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (allPlayers.Count > 1)
        {
            foreach (GameObject player in allPlayers)
            {
                if (!player.GetComponentInParent<PhotonView>().IsMine)
                {
                    DisableUI(player);
                }
            }

            if (allPlayers[turn] != localPlayer)
            {
                DisableUI(localPlayer);
            }
            else
            {
                EnableUI(localPlayer);
            }
        }

    }


    [PunRPC]
    public void RPC_IncrementTurn()
    {
        Debug.LogError($"Player is me before? {allPlayers[turn].GetComponentInParent<PhotonView>().IsMine}");
        Debug.LogError(turn);
        turn++;
        if (turn > allPlayers.Count - 1)
        {
            turn = 0;
        }
        Debug.LogError($"Player is me after? {allPlayers[turn].GetComponentInParent<PhotonView>().IsMine}");
        Debug.LogError(turn);

        Debug.LogError("Not my turn: " + (allPlayers[turn] != localPlayer));
        Debug.LogError("My turn: " + (allPlayers[turn] == localPlayer));

        if (allPlayers[turn] != localPlayer)
        {
            DrawCard(localPlayer);
        }
    }

    public void AddPlayersToList(GameObject player)
    {
        if (player.GetComponent<PhotonView>().IsMine)
        {
            localPlayer = player.transform.GetChild(0).gameObject;
        }

        allPlayers.Add(player.transform.GetChild(0).gameObject);
    }

    private void DrawCard(GameObject hand)
    {
        int percent = UnityEngine.Random.Range(0,100);
        GameObject newRoad;
        if(percent < probabilityBins[0]) //Road Block
        {
            newRoad = Instantiate(roadBlock) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }
        else if(percent < probabilityBins[1]) //4 Way
        {
            newRoad = Instantiate(fourWay) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }
        else if(percent < probabilityBins[2]) //T Intersection
        {
            newRoad = Instantiate(tIntersection) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }
        else if(percent < probabilityBins[3]) //Bended Turn
        {
            newRoad = Instantiate(bendedTurn) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }
        else if(percent < probabilityBins[4]) //Staright
        {
            newRoad = Instantiate(straight) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }        
    }

    private void StartingDraw()
    {
        for (int i = 0; i < 7; i++)
        {   
            DrawCard(localPlayer);
        }

        // Debug.Log(allPlayers[turn].ToString());
        // Debug.Log(localPlayer.ToString());

        if (allPlayers[turn] == localPlayer)
        {
            EnableUI(localPlayer);
        }
        else
        {
            DisableUI(localPlayer);
        }
    }

    private void DisableUI(GameObject player)
    {
        player.SetActive(false);
    }

    private void EnableUI(GameObject player)
    {
        player.SetActive(true);
    }
}

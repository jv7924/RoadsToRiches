using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineTurnSystem : MonoBehaviour
{
    private GameObject localPlayer;

    public int[] probabilityBins = new int[5];
    
    public GameObject roadBlock, fourWay, tIntersection, bendedTurn, straight;

    private bool myTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 7; i++)
            {   
                DrawCard(localPlayer);
            }
        }
    }

    public void SetLocalPlayer(GameObject player)
    {
        localPlayer = player;
    }

    private void ChangeTurn()
    {
        if (myTurn)
        {
            EnableUI();
        }
        else
        {
            DisableUI();
        }
    }

    private void DrawCard(GameObject hand)
    {
        int percent = Random.Range(0,100);
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

    private void DisableUI()
    {
        localPlayer.transform.parent.gameObject.SetActive(false);
    }

    private void EnableUI()
    {
        localPlayer.transform.parent.gameObject.SetActive(true);
    }
}

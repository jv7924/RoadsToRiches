using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineTurnSystem : MonoBehaviour
{   
    [SerializeField]
    private int[] probabilityBins = new int[5];
    //[SerializeField] private GameObject hand;
    
    [SerializeField]
    public GameObject[] players;

    [SerializeField]
    private GameObject roadBlock, fourWay, tIntersection, bendedTurn, straight;

    [SerializeField]
    private Animation cardAnim;

    private int turn;

    void Start()
    {
        turn = 0;
        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                DrawCard(players[i]);
            }
        }
    }

    void Update()
    {
        if(turn == 0)
        {
            players[0].transform.parent.gameObject.SetActive(true);
            players[1].transform.parent.gameObject.SetActive(false);
        }
        else if(turn == 1)
        {
            players[0].transform.parent.gameObject.SetActive(false);
            players[1].transform.parent.gameObject.SetActive(true);
        }
    }

    public void ChangeTurn()
    {
        string anim = "Card Draw Player " + (turn + 1);
        cardAnim.Play(anim);
        DrawCard(players[turn]);
        turn += 1;
        if(turn >= players.Length)
        {
            turn = 0;
        }
    }

    void DrawCard(GameObject hand)
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
}
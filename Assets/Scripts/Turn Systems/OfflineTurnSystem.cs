using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineTurnSystem : TurnSystem
{   
    [SerializeField]
    private int[] probabilityBins;
    
    [SerializeField]
    public GameObject[] players;

    [SerializeField]
    private GameObject[] cards;

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
        for (int i = 1; i < players.Length; i++)
        {
            players[i].transform.parent.gameObject.SetActive(false);
        }
    }

    public override void ChangeTurn()
    {
        if(players.Length == 2)
        {
            string anim = "Card Draw Player " + (turn + 1);
            cardAnim.Play(anim);
        }
        else if(players.Length == 4)
        {
            string anim = "4P Card Draw Player " + (turn + 1);
            cardAnim.Play(anim);
        }
        DrawCard(players[turn]);
        players[turn].transform.parent.gameObject.SetActive(false);
        turn += 1;
        if(turn >= players.Length)
        {
            turn = 0;
        }
        players[turn].transform.parent.gameObject.SetActive(true);
    }

    void DrawCard(GameObject hand)
    {
        int percent = Random.Range(0,100);
        GameObject newRoad;
        if(percent < probabilityBins[0]) //Road Block
        {
            newRoad = Instantiate(cards[0]) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }
        else if(percent < probabilityBins[1]) //4 Way
        {
            newRoad = Instantiate(cards[1]) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }
        else if(percent < probabilityBins[2]) //T Intersection
        {
            newRoad = Instantiate(cards[2]) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }
        else if(percent < probabilityBins[3]) //Bended Turn
        {
            newRoad = Instantiate(cards[3]) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }
        else if(percent < probabilityBins[4]) //Straight
        {
            newRoad = Instantiate(cards[4]) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }
        else if(percent < probabilityBins[5]) //Bomb
        {
            newRoad = Instantiate(cards[5]) as GameObject;
            newRoad.transform.SetParent(hand.transform);
        }         
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineTurnSystem : MonoBehaviour
{   
    [SerializeField]
    private int[] probabilityBins;
    //[SerializeField] private GameObject hand;
    
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
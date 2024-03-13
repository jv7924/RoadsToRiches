using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SingleHandTurnSystem : TurnSystem
{   
    [SerializeField]
    private int[] probabilityBins;
    
    [SerializeField]
    private int players;

    [SerializeField]
    private GameObject hand;

    [SerializeField]
    private TextMeshProUGUI tmp;

    [SerializeField]
    private TextMeshProUGUI tmp2;

    [SerializeField]
    private GameObject[] cards;

    [SerializeField]
    private Animation cardAnim;

    [SerializeField]
    private Image casinoImage;

    [SerializeField]
    private Sprite p1Sprite;

    [SerializeField]
    private Sprite p2Sprite;

    [SerializeField]
    private Sprite p3Sprite;

    [SerializeField]
    private Sprite p4Sprite;

    private int turn;

    void Start()
    {
        turn = 0;
        for(int j = 0; j < 5; j++)
        {
            DrawCard(hand);
        }
    }

    public override void ChangeTurn()
    {
        cardAnim.Play("Card Draw Player 1");
        DrawCard(hand);
        turn += 1;
        if(turn >= players)
        {
            turn = 0;
        }
        tmp.text = "Player " + (turn + 1);
        tmp2.text = "Player " + (turn + 1);

        // Change the casino sprite
        if (turn == 0)
        {
            casinoImage.sprite = p1Sprite;
        }
        else if (turn == 1)
        {
            casinoImage.sprite = p2Sprite;
        }
        else if (turn == 2)
        {
            casinoImage.sprite = p3Sprite;
        }
        else if (turn == 3)
        {
            casinoImage.sprite = p4Sprite;
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
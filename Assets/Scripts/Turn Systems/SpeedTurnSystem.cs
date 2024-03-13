using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedTurnSystem : TurnSystem
{
    [SerializeField]
    private int[] probabilityBins;
    
    [SerializeField]
    public GameObject[] players;

    [SerializeField]
    private GameObject[] cards;

    [SerializeField]
    private Animation cardAnim;

    [SerializeField]
    private GameObject discardPile;

    [SerializeField]
    private TMP_Text timerText;

    [SerializeField]
    private float turnTime;

    private float skipTime;

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
        discardPile = GameObject.FindWithTag("Discard Pile");
        skipTime = Time.time + turnTime;
    }

    void Update()
    {
        if(Time.time >= skipTime)
        {
            SkipTurn();
        }
        if(players[turn].transform.childCount == 0)
        {
            SkipTurn();
        }
        timerText.SetText((Mathf.Round((skipTime - Time.time) * 100) / 100.0).ToString());
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
        skipTime = Time.time + turnTime;
    }

    public void SkipTurn()
    {
        int handLength = players[turn].transform.childCount;
        if(handLength > 0)
        {
            int removeIndex = Random.Range(0, handLength);
            players[turn].transform.GetChild(removeIndex).SetParent(discardPile.transform);
        }
        players[turn].transform.parent.gameObject.SetActive(false);
        turn += 1;
        if(turn >= players.Length)
        {
            turn = 0;
        }
        players[turn].transform.parent.gameObject.SetActive(true);
        skipTime = Time.time + turnTime;
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

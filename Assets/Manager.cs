using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private int numPlayers = 2;
    [SerializeField] private int[] probabilities = new int[5];
    //[SerializeField] private GameObject hand;
    public GameObject[] players;
    private GameObject currentTurn;
    private int[] probabilityNumbers = new int[5];
    private int newNumber;
    public GameObject roadBlock;
    public GameObject fourWay;
    public GameObject tIntersection;
    public GameObject bendedTurn;
    public GameObject straight;
    private GameObject hand;

    void Start()
    {
        hand = GameObject.Find("Hand");
        DetermineProbabilities();
        for (int i = 0; i < 7; i++)
        {
            DrawCard();
        }
        //currentTurn = players[0];
    }

    void Update()
    {
        /*if (Input.GetKeyDown("a"))
        {
            DrawCard();
        }*/
        while (hand.transform.childCount < 7)
        {
            DrawCard();
        }
    }

    void ChangeTurn()
    {
        if (numPlayers == 2)
        {
            if (currentTurn = players[0])
            {
                currentTurn = players[1];
            } else {
                currentTurn = players[0];
            }
        } else if (numPlayers == 4)
        {
            if (currentTurn == players[0])
            {
                currentTurn = players[1];
            } else if (currentTurn == players[1])
            {
                currentTurn = players[2];
            } else if (currentTurn == players[2])
            {
                currentTurn = players[3];
            }
            else if (currentTurn == players[3])
            {
                currentTurn = players[0];
            }
        }
    }

    void DrawCard()
    {
        int number = Random.Range(1,101);
        GameObject newRoad;
        if ((1 <= number) && (number < probabilityNumbers[0])) //Road Block
        {
            newRoad = Instantiate(roadBlock) as GameObject;
            newRoad.transform.SetParent(hand.transform);
            //Debug.Log("RoadBlock");
        } else if ((probabilityNumbers[0] <= number) && (number < probabilityNumbers[1])) //4 Way
        {
            newRoad = Instantiate(fourWay) as GameObject;
            newRoad.transform.SetParent(hand.transform);
            //Debug.Log("4Way");
        } else if ((probabilityNumbers[1] <= number) && (number < probabilityNumbers[2])) //T Intersection
        {
            newRoad = Instantiate(tIntersection) as GameObject;
            newRoad.transform.SetParent(hand.transform);
           //Debug.Log("T");
        } else if ((probabilityNumbers[2] <= number) && (number < probabilityNumbers[3])) //Bended Turn
        {
            newRoad = Instantiate(bendedTurn) as GameObject;
            newRoad.transform.SetParent(hand.transform);
            //Debug.Log("Turn");
        } else if ((probabilityNumbers[3] <= number) && (number < probabilityNumbers[4])) //Straight
        {
            newRoad = Instantiate(straight) as GameObject;
            newRoad.transform.SetParent(hand.transform);
            //Debug.Log("Straight");
        }
    }

    void DetermineProbabilities()
    {
        for (int i = 0; i < probabilities.Length; i++)
        {
            if (i == 0)
            {
                newNumber = 1 + probabilities[i];
            } else {
                newNumber = newNumber + probabilities[i];
            }
            probabilityNumbers[i] = newNumber;
        }
    }
}

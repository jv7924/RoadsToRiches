using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WinnerTextHandler : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text winnerText;
    private int winnerNum = 0;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        //winnerNum = gridManager.CheckIfWon();
        titleText.text = "Player " + winnerNum.ToString() + " wins!";
        winnerText.text = "Player " + winnerNum.ToString() + " successfully connected the airport to their casino first!";
    }
}

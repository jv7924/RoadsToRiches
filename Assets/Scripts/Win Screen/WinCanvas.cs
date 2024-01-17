using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinCanvas : MonoBehaviour
{
    public TMP_Text win_text;

    public void UpdateText(int playerNumber)
    {
        win_text.SetText($"Player {playerNumber} has won!");
    }
}

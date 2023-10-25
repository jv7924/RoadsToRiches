using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField]
    private Tile hand;

    void pickupTile(Tile tile)
    {
        hand = tile;
    }

    void clearHand()
    {
        hand = null;
    }
}

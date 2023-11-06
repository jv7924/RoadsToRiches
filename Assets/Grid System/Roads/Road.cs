using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    // Stores a KVP for each of the four directions the tile connects.
    // The key stores a boolean where true means the direction has a valid connection point
    // and false means that direction cannot be connected to.
    // The value stores a reference to the connected road's GameObject if there is a connection
    // otherwise the value will be null.
    protected KeyValuePair<bool, GameObject> up;
    protected KeyValuePair<bool, GameObject> down;
    protected KeyValuePair<bool, GameObject> left;
    protected KeyValuePair<bool, GameObject> right;
    // Stores the rotation of the road in degrees
    // Starts at 0 and increases by 90 in the counter-clockwise direction
    protected int rotation;

    [SerializeField]
    public Material roadSprite;

    // Rotates the roads directions by 90 degrees clockwise
    public void RotateClock()
    {
        KeyValuePair<bool, GameObject> tempU = up;
        KeyValuePair<bool, GameObject> tempD = down;
        KeyValuePair<bool, GameObject> tempL = left;
        KeyValuePair<bool, GameObject> tempR = right;

        up = tempL;
        down = tempR;
        left = tempD;
        right = tempU;
        
        if (rotation == 0) { rotation = 270; }
        else { rotation -= 90; }
    }

    // Rotates the roads directions by 90 degrees counter-clockwise
    public void RotateCounterClock()
    {
        KeyValuePair<bool, GameObject> tempU = up;
        KeyValuePair<bool, GameObject> tempD = down;
        KeyValuePair<bool, GameObject> tempL = left;
        KeyValuePair<bool, GameObject> tempR = right;

        up = tempR;
        down = tempL;
        left = tempU;
        right = tempD;
        
        if (rotation == 270) { rotation = 0; }
        else { rotation += 90; }
    }

    // Takes in the direction of THIS road and returns whether that direction has a possible connection
    public bool CheckIfPossibleConnection(string direction)
    {
        if (direction == "up")
        {
            if (up.Key == true || up.Value == null) { return true; }
            else { return false; }
        }
        else if (direction == "down")
        {
            if (down.Key == true || down.Value == null) { return true; }
            else { return false; }
        }
        else if (direction == "left")
        {
            if (left.Key == true || left.Value == null) { return true; }
            else { return false; }
        }
        else if (direction == "right")
        {
            if (right.Key == true || right.Value == null) { return true; }
            else { return false; }
        }
        else 
        { 
            Debug.Log("Invalid direction provided");
            return false;
        }
    }

    // Takes in the direction of THIS road that a road is being connected to and a reference to the road that's being connected.
    // Returns true if the connection was made successfully, returns false if there was an issue
    public bool ConnectRoad(string direction, GameObject road)
    {
        if (direction == "up")
        {
            if (CheckIfPossibleConnection(direction) == true)
            {
                up = new KeyValuePair<bool, GameObject>(true, road);
                return true;
            }
            else { return false; }
        }
        else if (direction == "down")
        {
            if (CheckIfPossibleConnection(direction) == true)
            {
                down = new KeyValuePair<bool, GameObject>(true, road);
                return true;
            }
            else { return false; }
        }
        else if (direction == "left")
        {
            if (CheckIfPossibleConnection(direction) == true)
            {
                left = new KeyValuePair<bool, GameObject>(true, road);
                return true;
            }
            else { return false; }
        }
        else if (direction == "right")
        {
            if (CheckIfPossibleConnection(direction) == true)
            {
                right = new KeyValuePair<bool, GameObject>(true, road);
                return true;
            }
            else { return false; }
        }
        else 
        { 
            Debug.Log("Invalid direction provided");
            return false;
        }
    }

}

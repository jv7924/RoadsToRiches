using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using Photon.Pun;

public class GridManager : MonoBehaviour
{

    [SerializeField]
    private int width, height, camHeight, camOffset;

    [SerializeField]
    private Tile[] tilePrefabsEven;
    [SerializeField]
    private Tile[] tilePrefabsOdd;

    [SerializeField]
    private GameObject airport;

    [SerializeField]
    private GameObject[] casinos;

    [SerializeField]
    private Transform cam;

    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private AudioClip drawSound, buildSound, explosionSound;

    private Road[,] tiles;

    public PhotonView photonView;

    [SerializeField]
    private Road[] roads;

    [SerializeField]
    private GameObject discardPile;
    
    private int winningPlayerNumber;
    public GameObject WinCanvas;
    public GameObject player1Canvas = null;
    public GameObject player2Canvas = null;
    public GameObject player3Canvas = null;
    public GameObject player4Canvas = null;

    public GameObject turnSystem;

    System.Random random = new System.Random();

    public struct Coordinates
    {
        // Should only be able to get the value, not set it
        public int x { get; }
        public int y { get; }
        public Coordinates(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Road[width, height];
        GenerateGrid();

        photonView = GetComponent<PhotonView>();
    }

    public void GameWon(int playerNumber)
    {
        winningPlayerNumber = playerNumber;
    }

    void Update()   // Purely for debugging. Press enter to print out the whole array
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            PrintBoard();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (tiles[9, 9] is Casino)  // The game has been won
            {
                Debug.Log("YES, Thats a casino");
            }
        }

        //Check Winning Player
        if (winningPlayerNumber != 0)
        {
            WinCanvas.SetActive(true);
            if (player1Canvas != null) {
                player1Canvas.SetActive(false);
            }
            if (player2Canvas != null) {
                player2Canvas.SetActive(false);
            }
            if (player3Canvas != null) {
                player3Canvas.SetActive(false);
            }
            if (player4Canvas != null) {
                player4Canvas.SetActive(false);
            }
            turnSystem.SetActive(false);
            WinCanvas.GetComponent<WinCanvas>().UpdateText(winningPlayerNumber);
        }
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(((x + y) % 2 == 0) ? tilePrefabsEven[random.Next(0, 5)] : tilePrefabsOdd[random.Next(0, 5)], new Vector3(-x, 0, -y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.tag = "Board";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                if (casinos.Length == 2)
                {
                    if (x == 1 && y == 1)
                    {
                        var card = Instantiate(casinos[0]);
                        Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position, spawnedTile.transform.rotation);
                        addToList("Tile " + x + " " + y, card.GetComponent<Card>().road);
                        card.transform.SetParent(card.GetComponent<Card>().discardPile.transform);
                    }
                    else if (x == width - 2 && y == height - 2)
                    {
                        var card = Instantiate(casinos[1]);
                        Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position, spawnedTile.transform.rotation);
                        addToList("Tile " + x + " " + y, card.GetComponent<Card>().road);
                        card.transform.SetParent(card.GetComponent<Card>().discardPile.transform);
                    }
                    else if (x == width / 2 && y == height / 2)
                    {
                        var card = Instantiate(airport);
                        Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position, spawnedTile.transform.rotation);
                        addToList("Tile " + x + " " + y, card.GetComponent<Card>().road);
                        card.transform.SetParent(card.GetComponent<Card>().discardPile.transform);
                    }
                }
                else if (casinos.Length == 4)
                {
                    if (x == 1 && y == 1)
                    {
                        var card = Instantiate(casinos[0]);
                        Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position, spawnedTile.transform.rotation);
                        addToList("Tile " + x + " " + y, card.GetComponent<Card>().road);
                        card.transform.SetParent(card.GetComponent<Card>().discardPile.transform);
                    }
                    else if (x == width - 2 && y == 1)
                    {
                        var card = Instantiate(casinos[1]);
                        Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position, spawnedTile.transform.rotation);
                        addToList("Tile " + x + " " + y, card.GetComponent<Card>().road);
                        card.transform.SetParent(card.GetComponent<Card>().discardPile.transform);
                    }
                    else if (x == 1 && y == height - 2)
                    {
                        var card = Instantiate(casinos[2]);
                        Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position, spawnedTile.transform.rotation);
                        addToList("Tile " + x + " " + y, card.GetComponent<Card>().road);
                        card.transform.SetParent(card.GetComponent<Card>().discardPile.transform);
                    }
                    else if (x == width - 2 && y == height - 2)
                    {
                        var card = Instantiate(casinos[3]);
                        Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position, spawnedTile.transform.rotation);
                        addToList("Tile " + x + " " + y, card.GetComponent<Card>().road);
                        card.transform.SetParent(card.GetComponent<Card>().discardPile.transform);
                    }
                    else if (x == width / 2 && y == height / 2)
                    {
                        var card = Instantiate(airport);
                        Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position, spawnedTile.transform.rotation);
                        addToList("Tile " + x + " " + y, card.GetComponent<Card>().road);
                        card.transform.SetParent(card.GetComponent<Card>().discardPile.transform);
                    }
                }
            }
        }
        PositionCamera();
    }

    public void PrintBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y] != null)
                {
                    Debug.Log("(" + x + "," + y + ") = " + tiles[x, y] + tiles[x, y].up);
                }
            }
        }
    }

    public void PlayDrawSound()
    {
        audio.PlayOneShot(drawSound);
    }

    public void PlayBuildSound()
    {
        audio.PlayOneShot(buildSound);
    }

    public void PlayExplosionSound()
    {
        audio.PlayOneShot(explosionSound);
    }

    private void PositionCamera()
    {
        cam.transform.position = new Vector3(-(float)width / 2 + .5f, camHeight, -(float)height / 2 - .5f + camOffset);
        cam.transform.Rotate(new Vector3(90, 0, 180));
    }

    public bool addToList(string name, Road road)
    {
        string[] coords = name.Split(' ');
        int x = int.Parse(coords[1]);
        int y = int.Parse(coords[2]);
        if (tiles[x, y] != null)
        {
            return false;
        }
        else
        {
            tiles[x, y] = road;
            road.transform.SetParent(discardPile.transform);
            //Debug.Log("tile: " + tiles[x,y]);
            return true;
        }
    }

    public void DeleteFromList(GameObject boardTile)
    {
        Debug.Log("name" + boardTile.name);
        string[] coords = boardTile.name.Split(' ');
        int x = int.Parse(coords[1]);
        int y = int.Parse(coords[2]);
        tiles[x, y] = null;
        Debug.Log(tiles[x, y]);
    }

    [PunRPC]
    public void RPC_addToList(string name, string roadName, bool _up, bool _down, bool _left, bool _right, int _rotation)
    {
        // int timesRotated = rotation/90;
        // Debug.Log("Times rotated: " + timesRotated);
        string toRemove = "(Clone)";
        int i = roadName.IndexOf(toRemove);

        if (i >= 0)
        {
            roadName = roadName.Remove(i, toRemove.Length);
        }

        string[] coords = name.Split(' ');
        int x = int.Parse(coords[1]);
        int y = int.Parse(coords[2]);
        if (tiles[x, y] == null)
        {
            foreach (Road road in roads)
            {
                // Debug.Log("Condition is road.name == roadName: " + (road.name == roadName));

                if (road.name == roadName)
                {
                    Road roadClone = Instantiate(road);
                    Debug.Log(roadClone);
                    // roadClone.photonView.RPC("SyncValues", RpcTarget.All, up, down, left, right, rotation);
                    // roadClone

                    roadClone.up = new KeyValuePair<bool, Road>(_up, null);
                    roadClone.down = new KeyValuePair<bool, Road>(_down, null);
                    roadClone.left = new KeyValuePair<bool, Road>(_left, null);
                    roadClone.right = new KeyValuePair<bool, Road>(_right, null);
                    roadClone.rotation = _rotation;

                    tiles[x, y] = roadClone;

                    // Set keys here
                    Debug.Log("After sync------------------------------");
                    Debug.Log("Up: " + roadClone.up.Key);
                    Debug.Log("Down: " + roadClone.down.Key);
                    Debug.Log("Left: " + roadClone.left.Key);
                    Debug.Log("Right: " + roadClone.right.Key);
                    Debug.Log("Rotation: " + _rotation);
                    Debug.Log("----------------------------------------");

                    // Add to discard pile after everything
                    roadClone.transform.SetParent(discardPile.transform);
                }
            }
        }


    }

    public bool checkSurroundingCoords(string name, Road road)
    {
        checkRoad(road);

        string[] coords = name.Split(' ');
        int[] upCoords = new int[2];
        int[] rightCoords = new int[2];
        int[] downCoords = new int[2];
        int[] leftCoords = new int[2];
        int x = int.Parse(coords[1]);
        int y = int.Parse(coords[2]);
        if (tiles[x, y] == null)
        {
            //Get Coordinates of Surrounding Tiles
            if (y != 10) //Gets up coords
            {
                upCoords[0] = x;
                upCoords[1] = y + 1;
            }
            if (x != 10) //Gets right coords
            {
                rightCoords[0] = x + 1;
                rightCoords[1] = y;
            }
            if (y != 0) //Gets down coords
            {
                downCoords[0] = x;
                downCoords[1] = y - 1;
            }
            if (x != 0) //Gets left coords
            {
                leftCoords[0] = x - 1;
                leftCoords[1] = y;
            }

            //Check if all tiles are null
            if ((tiles[upCoords[0], upCoords[1]] == null) && (tiles[rightCoords[0], rightCoords[1]] == null) && (tiles[downCoords[0], downCoords[1]] == null) && (tiles[leftCoords[0], leftCoords[1]] == null))
            {
                Debug.Log("invalid placement: no surrounding tiles");
                return false;
            }

            bool oneConnection = false;
            bool canUp = false;
            bool canRight = false;
            bool canDown = false;
            bool canLeft = false;

            //Check directions
            if (tiles[upCoords[0], upCoords[1]] != null) //Check up
            {
                Road tempRoad = tiles[upCoords[0], upCoords[1]];
                if ((road.CheckIfPossibleConnection("up") == true) && (tempRoad.CheckIfPossibleConnection("down") == true))
                {
                    canUp = true;
                    oneConnection = true;
                }
                else if ((road.CheckIfPossibleConnection("up") == false) && (tempRoad.CheckIfPossibleConnection("down") == false))
                {
                    canUp = true;
                }
                else
                {
                    canUp = false;
                }
            }
            else
            {
                canUp = true;
            }

            if (tiles[rightCoords[0], rightCoords[1]] != null) //Check right
            {
                Road tempRoad = tiles[rightCoords[0], rightCoords[1]];
                if ((road.CheckIfPossibleConnection("right") == true) && (tempRoad.CheckIfPossibleConnection("left") == true))
                {
                    oneConnection = true;
                    canRight = true;
                }
                else if ((road.CheckIfPossibleConnection("right") == false) && (tempRoad.CheckIfPossibleConnection("left") == false))
                {
                    canRight = true;
                }
                else
                {
                    canRight = false;
                }
            }
            else
            {
                canRight = true;
            }

            if (tiles[downCoords[0], downCoords[1]] != null) //Check down
            {
                Road tempRoad = tiles[downCoords[0], downCoords[1]];
                if ((road.CheckIfPossibleConnection("down") == true) && (tempRoad.CheckIfPossibleConnection("up") == true))
                {
                    oneConnection = true;
                    canDown = true;
                }
                else if ((road.CheckIfPossibleConnection("down") == false) && (tempRoad.CheckIfPossibleConnection("up") == false))
                {
                    canDown = true;
                }
                else
                {
                    canDown = false;
                }
            }
            else
            {
                canDown = true;
            }

            if (tiles[leftCoords[0], leftCoords[1]] != null) //Check left
            {
                Road tempRoad = tiles[leftCoords[0], leftCoords[1]];
                if ((road.CheckIfPossibleConnection("left") == true) && (tempRoad.CheckIfPossibleConnection("right") == true))
                {
                    oneConnection = true;
                    canLeft = true;
                }
                else if ((road.CheckIfPossibleConnection("left") == false) && (tempRoad.CheckIfPossibleConnection("right") == false))
                {
                    canLeft = true;
                }
                else
                {
                    canLeft = false;
                }
            }
            else
            {
                canLeft = true;
            }

            // DEBUG STATEMENTS
            // Debug.Log("up " + tempRoad.CheckIfPossibleConnection("up").ToString());
            // Debug.Log("right " + tempRoad.CheckIfPossibleConnection("right").ToString());
            // Debug.Log("down " + tempRoad.CheckIfPossibleConnection("down").ToString());
            // Debug.Log("left " + tempRoad.CheckIfPossibleConnection("left").ToString());
            // Debug.Log("up " + canUp.ToString());
            // Debug.Log("right " + canRight.ToString());
            // Debug.Log("down " + canDown.ToString());
            // Debug.Log("left " + canLeft.ToString());

            if (canUp && canRight && canDown && canLeft && oneConnection) //Check all directions
            {
                Debug.Log("valid placement");
                return true;
            }
            else
            {
                Debug.Log("invalid placement: not all roads are connectable");
                return false;
            }
        }
        else
        {
            Debug.Log("something here, invalid placement");
            return false;
        }
    }

    public void checkRoad(Road road)
    {
        // Debug.Log("up " + road.CheckIfPossibleConnection("up").ToString());
        // Debug.Log("right " + road.CheckIfPossibleConnection("right").ToString());
        // Debug.Log("down " + road.CheckIfPossibleConnection("down").ToString());
        // Debug.Log("left " + road.CheckIfPossibleConnection("left").ToString());
    }

    // Takes in Coordinates and returns a list of Coordinates for all neighboring tiles that have a Road
    public List<Coordinates> GetNeighboringRoadCoords(Coordinates centerCoords)
    {
        List<Coordinates> neighboringTiles = new List<Coordinates>();
        // Check up
        if (centerCoords.y > 0)  // Make sure the index is not out of bounds
        {
            if (tiles[centerCoords.x, (centerCoords.y - 1)] is Road)  // Check if the index has a Road
            {
                neighboringTiles.Add(new Coordinates(centerCoords.x, centerCoords.y - 1));
            }
        }
        // Check down
        if (centerCoords.y < height - 1)
        {
            if (tiles[centerCoords.x, (centerCoords.y + 1)] is Road)
            {
                neighboringTiles.Add(new Coordinates(centerCoords.x, centerCoords.y + 1));
            }
        }
        // Check left
        if (centerCoords.x > 0)
        {
            if (tiles[(centerCoords.x - 1), centerCoords.y] is Road)
            {
                neighboringTiles.Add(new Coordinates(centerCoords.x - 1, centerCoords.y));
            }
        }
        // Check right
        if (centerCoords.x < width - 1)
        {
            if (tiles[(centerCoords.x + 1), centerCoords.y] is Road)
            {
                neighboringTiles.Add(new Coordinates(centerCoords.x + 1, centerCoords.y));
            }
        }
        return neighboringTiles;
    }

    // Takes in the coords for the parent road and a neigboring road and returns true if they have connecting streets
    public bool CheckIfRoadsAreConnected(Coordinates parentRoad, Coordinates neighboringRoad)
    {
        // Check if the neighboring road is up, down, left or right of the parent road
        if (neighboringRoad.y > parentRoad.y)  // Up
        {
            if (tiles[neighboringRoad.x, neighboringRoad.y].CheckIfPossibleConnection("down") == true &&
                tiles[parentRoad.x, parentRoad.y].CheckIfPossibleConnection("up") == true)
            {
                return true;
            }

        }
        else if (neighboringRoad.y < parentRoad.y)  // Down
        {
            if (tiles[neighboringRoad.x, neighboringRoad.y].CheckIfPossibleConnection("up") == true &&
                tiles[parentRoad.x, parentRoad.y].CheckIfPossibleConnection("down") == true)
            {
                return true;
            }
        }
        else if (neighboringRoad.x < parentRoad.x)  // Left
        {
            if (tiles[neighboringRoad.x, neighboringRoad.y].CheckIfPossibleConnection("right") == true &&
                tiles[parentRoad.x, parentRoad.y].CheckIfPossibleConnection("left") == true)
            {
                return true;
            }
        }
        else if (neighboringRoad.x > parentRoad.x)  // Right
        {
            if (tiles[neighboringRoad.x, neighboringRoad.y].CheckIfPossibleConnection("left") == true &&
                tiles[parentRoad.x, parentRoad.y].CheckIfPossibleConnection("right") == true)
            {
                return true;
            }
        }
        return false;  // Streets are not connected
    }

    // Returns the number of the player who has won the game or 0 if the game has not been won yet
    // Uses method overloading to pass in default values for the airport coordinates and an empty list
    public int CheckIfWon()
    {
        return CheckIfWon(new Coordinates(width / 2, height / 2), new List<Guid>());
    }

    // Recursive function that returns an int for the player who has won or 0 if the game has not been won
    // Can be called to start the win condition check from a specific coordinate instead of the airport
    public int CheckIfWon(Coordinates road, List<Guid> exploredRoads)
    {
        //Debug.Log($"Exploring coords ({road.x},{road.y})");
        if (tiles[road.x, road.y] is Casino)  // The game has been won
        {
            // Casino objects follow the naming convention of "CasinoX Card" where X is an int
            string pattern = @"\d+";  // Looks for one or more digits
            Match match = Regex.Match(tiles[road.x, road.y].name, pattern);  // Gets the first occurence (should only be one anyway)
            Debug.Log(int.Parse(match.Value));
            return int.Parse(match.Value);  // Returns the number of the casino respective to the winning player
        }

        if (exploredRoads.Contains(tiles[road.x, road.y].GetGuid()))  // Road has already been explored, return to prevent looping
        {
            return 0;
        }
        else
        {
            exploredRoads.Add(tiles[road.x, road.y].GetGuid());
        }

        int returnValue = 0;
        foreach (Coordinates coords in GetNeighboringRoadCoords(road))  // Iterate through all neighboring roads
        {
            // Make sure the roads are not only neighboring, but also have streets connecting
            if (CheckIfRoadsAreConnected(road, coords) == true)
            {
                returnValue = CheckIfWon(coords, exploredRoads);
                if (returnValue != 0) { break; }  // Break if we've found a casino, the game has been won
            }
        }
        return returnValue;
    }
}

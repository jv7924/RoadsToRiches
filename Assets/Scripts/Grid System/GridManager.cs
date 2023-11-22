using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int width, height, camHeight, camOffset;

    [SerializeField]
    private Tile tilePrefab;

    [SerializeField]
    private GameObject casino1, casino2, airport;

    [SerializeField]
    private Transform cam;

    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private AudioClip drawSound, buildSound;

    private Road[,] tiles;

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

        //TEMPORARY FIX FOR AIRPORT AND CASINOS
        tiles[1,1].up = new KeyValuePair<bool, Road>(true, null);
        tiles[1,1].right = new KeyValuePair<bool, Road>(true, null);
        tiles[1,1].down = new KeyValuePair<bool, Road>(true, null);
        tiles[1,1].left = new KeyValuePair<bool, Road>(true, null);
        tiles[5,5].up = new KeyValuePair<bool, Road>(true, null);
        tiles[5,5].right = new KeyValuePair<bool, Road>(true, null);
        tiles[5,5].down = new KeyValuePair<bool, Road>(true, null);
        tiles[5,5].left = new KeyValuePair<bool, Road>(true, null);
        tiles[9,9].up = new KeyValuePair<bool, Road>(true, null);
        tiles[9,9].right = new KeyValuePair<bool, Road>(true, null);
        tiles[9,9].down = new KeyValuePair<bool, Road>(true, null);
        tiles[9,9].left = new KeyValuePair<bool, Road>(true, null);
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
    }

    // Update is called once per frame
    void GenerateGrid()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(-x, 0, -y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.tag = "Board";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                if(x == 1 && y == 1)
                {
                    var card = Instantiate(casino1);
                    Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position + new Vector3(0, .05f, 0), spawnedTile.transform.rotation);
                    addToList("Tile " + x + " " + y, casino1.GetComponent<Card>().road);
                    card.transform.parent = card.GetComponent<Card>().discardPile.transform;
                    //Destroy(card);
                }
                else if(x == width - 2 && y == height - 2)
                {
                    var card = Instantiate(casino2);
                    Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position + new Vector3(0, .05f, 0), spawnedTile.transform.rotation);
                    addToList("Tile " + x + " " + y, casino2.GetComponent<Card>().road);
                    card.transform.parent = card.GetComponent<Card>().discardPile.transform;
                    //Destroy(card);
                }
                else if(x == width / 2 && y == height / 2)
                {
                    var card = Instantiate(airport);
                    Instantiate(card.GetComponent<Card>().tilePrefab, spawnedTile.transform.position + new Vector3(0, .05f, 0), spawnedTile.transform.rotation);
                    addToList("Tile " + x + " " + y, airport.GetComponent<Card>().road);
                    card.transform.parent = card.GetComponent<Card>().discardPile.transform;
                    //Destroy(card);
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
                Debug.Log("(" + x + "," + y + ") = " + tiles[x, y]);// + tiles[x,y].up);
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

    private void PositionCamera()
    {
        cam.transform.position = new Vector3(-(float)width / 2 - .5f, camHeight, -(float)height / 2 - .5f + camOffset);
        cam.transform.Rotate(new Vector3(90, 0, 180));
    }

    public bool addToList(string name, Road road)
    {
        string[] coords = name.Split(' ');
        int x = int.Parse(coords[1]);
        int y = int.Parse(coords[2]);
        if(tiles[x, y] != null)
        {
            return false;
        }
        else
        {
            tiles[x, y] = road;
            //Debug.Log("tile: " + tiles[x,y]);
            return true;
        }
    }

    public bool checkSurroundingCoords(string name, Road road)
    {
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
            if ((tiles[upCoords[0],upCoords[1]] == null) && (tiles[rightCoords[0],rightCoords[1]] == null) && (tiles[downCoords[0],downCoords[1]] == null) && (tiles[leftCoords[0],leftCoords[1]] == null))
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
            if (tiles[upCoords[0],upCoords[1]] != null) //Check up
            {
                Road tempRoad = tiles[upCoords[0],upCoords[1]];
                if ((road.CheckIfPossibleConnection("up") == true) && (tempRoad.CheckIfPossibleConnection("down") == true))
                {
                    canUp = true;
                    oneConnection = true;
                } else if ((road.CheckIfPossibleConnection("up") == false) && (tempRoad.CheckIfPossibleConnection("down") == false))
                {
                    canUp = true;
                } else 
                {
                    canUp = false;
                }
            } else 
            {
                canUp = true;
            }

            if (tiles[rightCoords[0],rightCoords[1]] != null) //Check right
            {
                Road tempRoad = tiles[rightCoords[0],rightCoords[1]];
                if ((road.CheckIfPossibleConnection("right") == true) && (tempRoad.CheckIfPossibleConnection("left") == true))
                {
                    oneConnection = true;
                    canRight = true;
                } else if ((road.CheckIfPossibleConnection("right") == false) && (tempRoad.CheckIfPossibleConnection("left") == false))
                {
                    canRight = true;
                } else 
                {
                    canRight = false;
                }
            }
             else 
            {
                canRight = true;
            }

            if (tiles[downCoords[0],downCoords[1]] != null) //Check down
            {
                Road tempRoad = tiles[downCoords[0],downCoords[1]];
                if ((road.CheckIfPossibleConnection("down") == true) && (tempRoad.CheckIfPossibleConnection("up") == true))
                {
                    oneConnection = true;
                    canDown = true;
                } else if ((road.CheckIfPossibleConnection("down") == false) && (tempRoad.CheckIfPossibleConnection("up") == false))
                {
                    canDown = true;
                } else 
                {
                    canDown = false;
                }
            }
             else 
            {
                canDown = true;
            }

            if (tiles[leftCoords[0],leftCoords[1]] != null) //Check left
            {
                Road tempRoad = tiles[leftCoords[0],leftCoords[1]];
                if ((road.CheckIfPossibleConnection("left") == true) && (tempRoad.CheckIfPossibleConnection("right") == true))
                {
                    oneConnection = true;
                    canLeft = true;
                } else if ((road.CheckIfPossibleConnection("left") == false) && (tempRoad.CheckIfPossibleConnection("right") == false))
                {
                    canLeft = true;
                } else 
                {
                    canLeft = false;
                }
            } else 
            {
                canLeft = true;
            }

            /*Road tempRoad2 = tiles[x,y];
            Debug.Log("up " + tempRoad2.CheckIfPossibleConnection("up").ToString());
            Debug.Log("right " + tempRoad2.CheckIfPossibleConnection("right").ToString());
            Debug.Log("down " + tempRoad2.CheckIfPossibleConnection("down").ToString());
            Debug.Log("left " + tempRoad2.CheckIfPossibleConnection("left").ToString());*/

                //DEBUG STATEMENTS
                //Debug.Log("up " + tempRoad.CheckIfPossibleConnection("up").ToString());
                //Debug.Log("right " + tempRoad.CheckIfPossibleConnection("right").ToString());
                //Debug.Log("down " + tempRoad.CheckIfPossibleConnection("down").ToString());
                //Debug.Log("left " + tempRoad.CheckIfPossibleConnection("left").ToString());
                //Debug.Log("up " + canUp.ToString());
                //Debug.Log("right " + canRight.ToString());
                //Debug.Log("down " + canDown.ToString());
                //Debug.Log("left " + canLeft.ToString());
            
            if (canUp && canRight && canDown && canLeft && oneConnection) //Check all directions
            {
                Debug.Log("valid placement");
                return true;
            } else
            {
                Debug.Log("invalid placement: not all roads are connectable");
                return false;
            }
        } else 
        {
            Debug.Log("something here, invalid placement");
            return false;
        }
    }

    public void checkRoad(Road road)
    {
        Debug.Log("up " + road.CheckIfPossibleConnection("up").ToString());
        Debug.Log("right " + road.CheckIfPossibleConnection("right").ToString());
        Debug.Log("down " + road.CheckIfPossibleConnection("down").ToString());
        Debug.Log("left " + road.CheckIfPossibleConnection("left").ToString());
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

    // Returns the number of the player who has won the game or 0 if the game has not been won yet
    // Uses method overloading to pass in default values for the airport coordinates and an empty list
    public int CheckIfWon()
    {
        return CheckIfWon(new Coordinates(width/2, height/2), new List<Guid>());
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
            return int.Parse(match.Value);  // Returns the number of the casino respective to the winning player
        }

        if (exploredRoads.Contains( tiles[road.x, road.y].GetGuid() ))  // Road has already been explored, return to prevent looping
        {
            return 0;
        }
        else
        {
            exploredRoads.Add( tiles[road.x, road.y].GetGuid() );
        }

        int returnValue = 0;
        foreach (Coordinates coords in GetNeighboringRoadCoords(road))  // Iterate through all neighboring roads
        {
            returnValue = CheckIfWon(coords, exploredRoads);
            if (returnValue != 0) { break; }  // Break if we've found a casino, the game has been won
        }
        return returnValue;
    }
}

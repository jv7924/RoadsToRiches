using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Road[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Road[width, height];
        GenerateGrid();
    }

    // Update is called once per frame
    void GenerateGrid()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.tag = "Board";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                if(x == 1 && y == 1)
                {
                    Instantiate(casino1.GetComponent<Card>().tilePrefab, spawnedTile.transform.position + new Vector3(0, .05f, 0), spawnedTile.transform.rotation);
                    addToList("Tile " + x + " " + y, casino1.GetComponent<Card>().road);
                }
                else if(x == width - 2 && y == height - 2)
                {
                    Instantiate(casino2.GetComponent<Card>().tilePrefab, spawnedTile.transform.position + new Vector3(0, .05f, 0), spawnedTile.transform.rotation);
                    addToList("Tile " + x + " " + y, casino2.GetComponent<Card>().road);
                }
                else if(x == width / 2 && y == height / 2)
                {
                    Instantiate(airport.GetComponent<Card>().tilePrefab, spawnedTile.transform.position + new Vector3(0, .05f, 0), spawnedTile.transform.rotation);
                    addToList("Tile " + x + " " + y, airport.GetComponent<Card>().road);
                }
            }
        }
        PositionCamera();
    }

    private void PositionCamera()
    {
        cam.transform.position = new Vector3((float)width / 2 - .5f, camHeight, (float)height / 2 - .5f + camOffset);
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
            return true;
        }
    }

    public bool checkSurroundingCoords(string name)
    {
        string[] coords = name.Split(' ');
        int[] upCoords = new int[2];
        int[] rightCoords = new int[2];
        int[] downCoords = new int[2];
        int[] leftCoords = new int[2];
        int x = int.Parse(coords[1]);
        int y = int.Parse(coords[2]);
        /*for (int i = 0; i < 11; i++)
        {
            for (int a = 0; a < 11; a++)
            {
                //Debug.Log(tiles[i,a]);
            }
        }*/
        if (tiles[x, y] == null)
        {
            //Get Coordinates of Surrounding Tiles
            if (y != 0) //Gets up coords
            {
                upCoords[0] = x;
                upCoords[1] = y - 1;
                //Debug.Log("up " + upCoords[0].ToString() + " " + upCoords[1].ToString());
            }
            if (x != 0) //Gets right coords
            {
                rightCoords[0] = x - 1;
                rightCoords[1] = y;
                //Debug.Log("right " + rightCoords[0].ToString() + " " + rightCoords[1].ToString());
            }
            if (y != 10) //Gets down coords
            {
                downCoords[0] = x;
                downCoords[1] = y + 1;
                //Debug.Log("down " + downCoords[0].ToString() + " " + downCoords[1].ToString());
            }
            if (x != 10) //Gets left coords
            {
                leftCoords[0] = x + 1;
                leftCoords[1] = y;
                //Debug.Log("left " + leftCoords[0].ToString() + " " + leftCoords[1].ToString());
            }

            if ((tiles[upCoords[0],upCoords[1]] == null) && (tiles[rightCoords[0],rightCoords[1]] == null) && (tiles[downCoords[0],downCoords[1]] == null) && (tiles[leftCoords[0],leftCoords[1]] == null))
            {
                //Debug.Log("Invalid");
                return false;
            }
            //Debug.Log(name);
            return true;
        } else 
        {
            Debug.Log("something here, invalid placement");
            return false;
        }
    }
}

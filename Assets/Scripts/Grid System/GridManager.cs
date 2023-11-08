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
}

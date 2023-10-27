using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int width, height, camHeight;

    [SerializeField]
    private Tile tilePrefab;

    [SerializeField]
    private Transform cam;

    private GameObject[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        tiles = new GameObject[width, height];
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
            }
        }
        cam.transform.position = new Vector3((float)width / 2 - .5f, camHeight, (float)height / 2 - .5f);
        cam.transform.Rotate(new Vector3(90, 0, 0));
    }

    public bool addToList(string name, GameObject card)
    {
        Debug.Log(name + card.name);
        string[] coords = name.Split(' ');
        int x = int.Parse(coords[1]);
        int y = int.Parse(coords[2]);
        if(tiles[x, y] != null)
        {
            return false;
        }
        else
        {
            tiles[x, y] = card; 
            return true;
        }
    }
}

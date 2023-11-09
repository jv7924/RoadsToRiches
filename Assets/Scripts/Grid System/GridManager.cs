using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int width, height, camHeight;

    [SerializeField]
    private Tile tilePrefab;

    [SerializeField]
    private Transform cam;

    private Road[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        // if (PhotonNetwork.IsMasterClient)
        // {
            GenerateGrid();
            tiles = new Road[width, height];
        // }
        // else 
        // {
        //     SetCamera();
        // }
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
        SetCamera();
    }

    private void SetCamera()
    {
        cam.transform.position = new Vector3((float)width / 2 - .5f, camHeight, (float)height / 2 - .5f);
        cam.transform.Rotate(new Vector3(90, 0, 180));
    }

    public bool addToList(string name, Road road)
    {
        string[] coords = name.Split(' ');
        Debug.LogError(name + ", " + road.ToString());
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

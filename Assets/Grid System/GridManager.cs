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

    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
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
            }
        }
        cam.transform.position = new Vector3((float)width / 2 - .5f, camHeight, (float)height / 2 - .5f);
        cam.transform.Rotate(new Vector3(90, 0, 0));
    }
}

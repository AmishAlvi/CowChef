using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Draggable MirrorPrefab;
    [SerializeField] private Transform cam;
    public Vector3 mirrorLocation; 

    private void Start()
    {
        GenerateGrid();
        PlaceMirrors();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x,y, 0), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = transform;

                var isOffset = (x % 2 == 0 && y%2 != 0) || (x %2 != 0 && y%2 == 0);
                Debug.Log(isOffset);
                spawnedTile.Init(isOffset);
            }
        }

        cam.transform.position = new Vector3((float) width/2 -0.5f , (float)height / 2 - 0.5f, -10);
    }

    void PlaceMirrors()
    {
        var spawnedMirror = Instantiate(MirrorPrefab, mirrorLocation, Quaternion.identity);
    }
}

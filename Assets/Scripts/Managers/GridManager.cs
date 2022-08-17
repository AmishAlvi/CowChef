using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private GameObject MirrorPrefab;
    [SerializeField] private Draggable draggablePrefab;
    [SerializeField] private laser laser;
    [SerializeField] private Transform cam;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject barrierPrefab;

    public Level level;
    public Vector3 mirrorLocation;
    private List<Food> ingredients;
    private List<Tile> tiles;

    private void Awake()
    {
        width = level.cols;
        height = level.rows;
    }

    private void Start()
    {
        
    }

    public void InitializeLevel()
    {
        ingredients = new List<Food>();
        tiles = new List<Tile>();
        GenerateGrid();
        PlaceMirrors();
        PlaceLaser();
        PlaceFood();
        SortFood();
       // Debug.Log(ingredients);
    }

    public void PrintFood(List<Food> Food)
    {
        foreach (Food f in Food)
        {
            Debug.Log(f.GetName());
        }
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
                //Debug.Log(isOffset);
                spawnedTile.Init(isOffset);
                tiles.Add(spawnedTile);
            }
        }

        for(int y = -1; y < height + 1; y++)
        {
            Instantiate(barrierPrefab, new Vector3(-1, y, 0), Quaternion.identity);
            if (y == height || y == -1)
            {
                for(int x = 0; x < width; x++)
                {
                    Instantiate(barrierPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
            Instantiate(barrierPrefab, new Vector3(width, y, 0), Quaternion.identity);

        }

        cam.transform.position = new Vector3((float) width/2 -2f , (float)height / 2 - 0.5f, -10);
    }

    void PlaceMirrors()
    {
        foreach(Level.Mirror mirror in level.mirrors)
        {
            Draggable draggableParent = Instantiate(draggablePrefab, mirror.location, Quaternion.identity);
            GameObject mirrorChild = Instantiate(MirrorPrefab, mirror.location, Quaternion.Euler(mirror.rotation)) as GameObject;

            mirrorChild.transform.parent = draggableParent.transform;

        }

    }

    void PlaceLaser()
    {
        var spawnedLaser = Instantiate(laser, level.laserLocation, Quaternion.identity);
        spawnedLaser.direction = level.laserDirection;
        RemoveTileBelow(level.laserLocation);
    }

    void PlaceFood()
    {
        foreach (Level.FoodItem food in level.food)
        {
            var spawnedFood = Instantiate(level.foodPrefab, food.location, Quaternion.identity);
            spawnedFood.GetComponent<SpriteRenderer>().sprite = food.sprite;
            Food spFood = spawnedFood.GetComponent<Food>();
            spFood.setOrder(food.orderNumber);
            spFood.SetName(food.name);
            ingredients.Add(spFood);
            RemoveTileBelow(food.location);
        }
    }

    public void SortFood()
    {
        List<Food> sortedFood =
            ingredients.OrderBy(order => order.getOrder()).ToList();

        ingredients = sortedFood;
    }

    public List<Food> GetFood()
    {
        return ingredients;
    }

    private void RemoveTileBelow(Vector2 location)
    {
        GameObject TileBelow = tiles.Find(tile => tile.gameObject.name.Equals($"Tile {location.x} {location.y}")).gameObject;
        TileBelow.SetActive(false);
    }
}

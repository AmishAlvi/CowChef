using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Draggable MirrorPrefab;
    [SerializeField] private laser laser;
    [SerializeField] private Transform cam;
    [SerializeField] private GameManager gameManager;

    public Level level;
    public Vector3 mirrorLocation;
    private List<Food> ingredients;

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
                Debug.Log(isOffset);
                spawnedTile.Init(isOffset);
            }
        }

        cam.transform.position = new Vector3((float) width/2 -0.5f , (float)height / 2 - 0.5f, -10);
    }

    void PlaceMirrors()
    {
        foreach(Level.Mirror mirror in level.mirrors)
        {
            Instantiate(MirrorPrefab, mirror.location, Quaternion.Euler(mirror.rotation));
        }

    }

    void PlaceLaser()
    {
        var spawnedLaser = Instantiate(laser, level.laserLocation, Quaternion.identity);
        spawnedLaser.direction = level.laserDirection;
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
}

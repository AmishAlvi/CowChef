using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Food> Ingredients;
    public List<string> AddedToDish;
    public Button laserButton, stopButton;
    public GameObject Laser;
    // Start is called before the first frame update
    void Start()
    {
        laserButton.onClick.AddListener(startLaser);
        stopButton.onClick.AddListener(stopLaser);
    }

    // Update is called once per frame
    void Update()
    {
       foreach(Food f in Ingredients)
        {
            if(f.isAdded && !AddedToDish.Contains(f.name))
            {
                AddedToDish.Add(f.name);
            }
        }

       if(AddedToDish.Count == 5)
        {
            CheckWin();
        }
    }

    void CheckWin()
    {
        string[] AddOrder = { "Sugar", "Maple", "CoconutMilk", "Vanilla", "Flan" };

        for(int i = 0; i<5; i++)
        {
            if(AddOrder[i] != AddOrder[i])
            {
                SceneManager.LoadScene(2);
            }
        }
        SceneManager.LoadScene(1);
    }

    void startLaser()
    {
        Laser.GetComponent<RayCaster>().enabled = true;
    }

    void stopLaser()
    {
        Laser.GetComponent<RayCaster>().enabled = false;
        Laser.GetComponent<LineRenderer>().enabled = false;
        AddedToDish.Clear();
        foreach (Food f in Ingredients)
        {
            f.isAdded = false;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Food> Ingredients;
    public List<string> AddedToDish;
    public List<Text> CookOrderTexts;
    public List<Image> CookOrderImages;
    //public Button laserButton, stopButton;
    public GameObject Laser;

    private int OrderCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //laserButton.onClick.AddListener(startLaser);
        //stopButton.onClick.AddListener(stopLaser);
    }

    // Update is called once per frame
    void Update()
    {
       foreach(Food f in Ingredients)
        {
            if(f.isAdded && !AddedToDish.Contains(f.name))
            {
                AddedToDish.Add(f.name);
                CookOrderTexts[OrderCount].text = f.name;
                CookOrderTexts[OrderCount].enabled = true;
                CookOrderImages[OrderCount].sprite = f.gameObject.GetComponent<SpriteRenderer>().sprite;
                CookOrderImages[OrderCount].enabled = true;
                Debug.Log("new food text: " + CookOrderTexts[OrderCount].text);
                OrderCount++;
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
        bool Won = true;
        for(int i = 0; i<5; i++)
        {
            if(!AddedToDish[i].Equals(AddOrder[i]))
            {
                Debug.Log(AddedToDish[i].Equals(AddOrder[i]));
                SceneManager.LoadScene(2);
                Won = false;
            }
        }

        if(Won)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
        
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


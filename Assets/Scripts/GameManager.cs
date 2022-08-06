using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IObserver<Food>
{
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GridManager GridManager;

    public Level level;


    public List<Food> Ingredients;
    public List<string> AddedToDish;
    public List<Text> CookOrderTexts;
    public List<Image> CookOrderImages;
    //public Button laserButton, stopButton;
    public GameObject Laser;

    private int OrderCount = 0;

    private void Awake()
    {
        GridManager.level = level;
    }

    // Start is called before the first frame update
    void Start()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);

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
                LosePanel.SetActive(true);
                Won = false;
            }
        }

        if(Won)
        {
            WinPanel.SetActive(true);
        }
        else
        {
            LosePanel.SetActive(true);
        }
        
    }

    void IObserver<Food>.OnCompleted()
    {
        throw new NotImplementedException();
    }

    void IObserver<Food>.OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    void IObserver<Food>.OnNext(Food value)
    {
        throw new NotImplementedException();
    }
}


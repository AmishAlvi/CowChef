using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour, Observer
{
   // public UnityEvent test;

    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GridManager GridManager;
    [SerializeField] private UIManager UIManager;

    public Level level;


   // public Food[] Ingredients;
    public List<Food> Foods;
    //public Button laserButton, stopButton;
    public GameObject Laser;

    public int NextFoodIndex = 0;

    private void Awake()
    {
        GridManager.level = level;
        

    }

    // Start is called before the first frame update
    void Start()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        GridManager.InitializeLevel();
        Foods = GridManager.GetFood();
        UIManager.InstantiateUI(Foods);
        foreach (Food f in Foods)
        {
            f.Subscribe(this);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NextFoodIndex >= Foods.Count)
        {
            CheckWin();
        }
    }

    void CheckWin()
    {
        bool Won = true;
        foreach(Food f in Foods)
        {
            if(f.isInOrder == false)
            {
                Won = false;
            }
        }

        if(Won)
        {
            WinPanel.SetActive(Won);
        }
        
    }

    void CheckOrder(Food CurrentFoodHit)
    {
        Debug.Log(CurrentFoodHit.getOrder() + " " + NextFoodIndex);
        switch (CurrentFoodHit.isAdded)
        {
            
            case true:
                if(CurrentFoodHit.getOrder() == NextFoodIndex)
                {
                    NextFoodIndex++;
                    Foods[CurrentFoodHit.getOrder()].isAdded = true;
                    Foods[CurrentFoodHit.getOrder()].isInOrder = true;
                    Debug.Log("Correct hit");
                    CurrentFoodHit.SetStatus(0);
                    break;
                }
                else
                {
                    Debug.LogWarning("Wrong hit");
                    Foods[CurrentFoodHit.getOrder()].isInOrder = false;
                    CurrentFoodHit.SetStatus(1);
                    break;
                }
            case false:
                NextFoodIndex--;
                if (NextFoodIndex < 0)
                {
                    NextFoodIndex = 0;
                }
                Foods[CurrentFoodHit.getOrder()].isAdded = false;
                Foods[CurrentFoodHit.getOrder()].isInOrder = false;
                break;
        }
    }

    public void Notify(Observable observable)
    {
        Food tempFood = (Food) observable;
        CheckOrder(tempFood);

    }
}


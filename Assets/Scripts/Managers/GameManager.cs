using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
//using System.Linq;

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
    private List<Food> tempCheckList;
    //public Button laserButton, stopButton;
    public GameObject Laser;

    //public int CurrentFoodPointer = -1;
    private int CurrentlyAdded = 0;

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
        if (CurrentlyAdded >= Foods.Count)
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
        Debug.Log("Food being added: " + CurrentFoodHit.GetName());
        if(CurrentFoodHit.isAdded)
        {            
           
                if(CurrentFoodHit.getOrder() == Foods[CurrentlyAdded].getOrder())
                {
                    Foods[CurrentlyAdded].isAdded = true;
                    Foods[CurrentlyAdded].isInOrder = true;
                    Debug.Log("Correct hit"); 
                    CurrentFoodHit.SetStatus(0);
                    
                }
                else
                {
                   // CurrentFoodPointer++;
                    Debug.LogWarning("Wrong hit");
                    Foods[CurrentFoodHit.getOrder()].isInOrder = false;
                    CurrentFoodHit.SetStatus(1);
                    
                }
            
            Debug.Log(Foods.FindAll(food => food.isAdded == true).Count);
            CurrentlyAdded = Foods.FindAll(food => food.isAdded == true).Count;


        }
        else
        {
            Foods[CurrentFoodHit.getOrder()].isAdded = false;
            Foods[CurrentFoodHit.getOrder()].isInOrder = false;
            Debug.Log(Foods.FindAll(food => food.isAdded == true).Count);
            CurrentlyAdded = Foods.FindAll(food => food.isAdded == true).Count;
        }

        
    }

    public void Notify(Observable observable)
    {
        Food tempFood = (Food) observable;
        CheckOrder(tempFood);

    }
}


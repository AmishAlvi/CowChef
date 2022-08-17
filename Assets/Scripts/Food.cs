using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour, Observable
{
    public bool isAdded, isInOrder;
    private List<Observer> observers;
    private int orderInRecipe;
    private string ingredientName;
    private Text ingredientText;
    public Image ingredientImage, ingredientTickBox;

    [SerializeField] Sprite tick, cross;
    [SerializeField] AudioClip correct, wrong;
    

    private void Awake()
    {
        isAdded = false;
        observers = new List<Observer>();
    }

   /* private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "laser")
        {
            isAdded = true;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="laser")
        {
            isAdded = true;
            ingredientTickBox.sprite = tick;
            ingredientTickBox.color = Color.white;
           // Debug.Log("notification sent by on enter");
            NotifySubscribers();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "laser")
        {
            isAdded = false;
            ingredientTickBox.color = Color.clear;
            //Debug.Log("notification sent by on exit");
            NotifySubscribers();
        }
    }

    public void Subscribe(Observer observer)
    {
        observers.Add(observer);
    }

    private void NotifySubscribers()
    {
        foreach(Observer observer in observers)
        {
            observer.Notify(this);
        }
    }

    public void setOrder(int order)
    {
        orderInRecipe = order;
    }

    public int getOrder()
    {
        return orderInRecipe;
    }

    public void SetName(string newName)
    {
        ingredientName = newName;
    }

    public string GetName()
    {
        return ingredientName;
    }

    public void SetUI(Text text, Image icon, Image tickBox)
    {
        ingredientText = text;
        ingredientImage = icon;
        ingredientTickBox = tickBox;
    }

    public void SetStatus(int status)
    {
        switch (status)
        {
            case 0: // Correct
                ingredientTickBox.sprite = tick;
                ingredientTickBox.color = Color.white;
                isInOrder = true;
                AudioManager.instance.PlaySound(correct, 0.5f);
                break;
            case 1: //Wrong
                ingredientTickBox.sprite = cross;
                ingredientTickBox.color = Color.white;
                isInOrder=false;
                AudioManager.instance.PlaySound(wrong, 0.5f);
                break;

        }
    }

}

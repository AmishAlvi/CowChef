using System;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, Observable
{
    public bool isAdded;
    private List<Observer> observers;
    private int orderInRecipe;

    private void Awake()
    {
        isAdded = false;
        observers = new List<Observer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "laser")
        {
            isAdded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="laser")
        {
            isAdded = true;
            NotifySubscribers();
            Debug.Log("notification sent");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "laser")
        {
            isAdded = false;
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
}

using System;
using UnityEngine;

public class Food : MonoBehaviour, IObservable<Food>
{
    public bool isAdded;
    public static event Action foodCooked;

    private void Awake()
    {
        isAdded = false;
    }

    private void Added()
    {
        foodCooked?.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "laser")
        {
            isAdded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "laser")
        {
            isAdded = false;
        }
    }

    IDisposable IObservable<Food>.Subscribe(IObserver<Food> observer)
    {
        throw new NotImplementedException();
    }
}

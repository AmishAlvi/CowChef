using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public bool isAdded, isBeingAdded;

    private void Awake()
    {
        isAdded = false;
    }

    private void Update()
    {
       
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


}

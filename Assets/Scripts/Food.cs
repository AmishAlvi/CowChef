using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public bool isAdded, isBeingAdded;
    public Slider progressSlider;
    // Start is called before the first frame update
    private void Awake()
    {
        isAdded = false;
    }

    private void Update()
    {
        if(isBeingAdded)
        {
            progressSlider.gameObject.SetActive(true);
            progressSlider.value += 0.5f;
        }
        else
        {
            progressSlider.gameObject.SetActive(false);
            progressSlider.value = 0;
        }

        if(progressSlider.value == progressSlider.maxValue)
        {
            progressSlider.gameObject.SetActive(false);
            isAdded = true;
        }
       
    }

}

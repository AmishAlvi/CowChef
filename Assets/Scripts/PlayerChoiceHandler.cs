using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceHandler : MonoBehaviour
{
    public void SaveRecipeChoice(string choice)
    {
        PlayerPrefs.SetString("choice", choice);
    }

    public string GetRecipeChoice()
    {
        return PlayerPrefs.GetString("choice");
    }
}

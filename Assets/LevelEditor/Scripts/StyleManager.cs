using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleManager : MonoBehaviour
{
    public ButtonStyle[] buttonStyles;
}

[System.Serializable]
public struct ButtonStyle
{
    public Texture2D Icon;
    public string ButtonTex;
    [HideInInspector]
    public GUIStyle NodeStyle;
}   

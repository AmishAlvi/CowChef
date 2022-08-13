using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool Occupied = false;

    public void Init(bool isOffset)
    {
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }

    public bool IsOccupied()
    {
        return Occupied;
    }

    public void Occupy(bool status)
    {
        Occupied = status;
    }
    

}

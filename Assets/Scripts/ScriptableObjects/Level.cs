using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Level", menuName ="Level")]
public class Level : ScriptableObject
{
    public string name;

    public int rows, cols;
    public GameObject mirrorPrefab;
    public GameObject foodPrefab;
    public List<Mirror> mirrors = new List<Mirror>();


    // location and rotation of laser
    public Vector2 laserDirection;
    public Vector2 laserLocation;

    // location and sprite of food
    public Vector2 foodLocation;
    public Sprite foodSprite;


    //class to put mirrors in mirror list
    [System.Serializable]
    public class Mirror
    {
        public Vector3 rotation;
        public Vector2 location;
    }

}

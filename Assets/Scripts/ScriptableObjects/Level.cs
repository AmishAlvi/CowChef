using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Level", menuName ="Level")]
public class Level : ScriptableObject
{
    public string name;

    public int rows, cols;
    public List<Mirror> mirrors = new List<Mirror>();
    public GameObject mirrorPrefab;

    [System.Serializable]
    public class Mirror
    {
        public Vector3 rotation;
        public Vector2 location;
    }

}

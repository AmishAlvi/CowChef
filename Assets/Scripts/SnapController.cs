using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    private List<Tile> snapPoints;
    private List<Draggable> draggableObjects;

    public float snapRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        snapPoints = new List<Tile>();
        draggableObjects = new List<Draggable>();

        foreach (Draggable dg in FindObjectsOfType(typeof(Draggable)))
        {
            draggableObjects.Add(dg);
        }

        foreach (Tile t in FindObjectsOfType<Tile>())
        {
            snapPoints.Add(t);
        }
        
        


        foreach(Draggable draggable in draggableObjects)
        {
            Tile closestSnapPoint = null;
            draggable.dragEndedCallback = onDragEnded;
            foreach (Tile snapPoint in snapPoints)
            {
                float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.transform.localPosition);
                if (closestSnapPoint == null || currentDistance < 1)
                {
                    closestSnapPoint = snapPoint;
                    closestSnapPoint.Occupy(true);
                    draggable.SetCurrentSnapPoint(closestSnapPoint);
                    
                }
            }
        }
    }

   private void onDragEnded(Draggable draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach(Tile snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.transform.localPosition);
            if(closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint.transform;
                closestDistance = currentDistance;
            }
        }

        if(closestSnapPoint != null && closestDistance <= snapRange && !closestSnapPoint.GetComponent<Tile>().IsOccupied())
        {
            draggable.GetCurrentSnapPoint().Occupy(false);
            draggable.transform.localPosition = closestSnapPoint.localPosition;
            draggable.SetCurrentSnapPoint(closestSnapPoint.GetComponent<Tile>());
            closestSnapPoint.GetComponent<Tile>().Occupy(true);
        }
        else
        {
            draggable.transform.localPosition = draggable.GetCurrentSnapPoint().transform.localPosition;//new Vector3(draggable.MouseDragStartPosition.x, draggable.MouseDragStartPosition.y, 0);
           // onDragEnded(draggable);
        }
    }

    //private void Snap(float closestDistance, Transform closestSnapPoint,)
    //{

    //}
}

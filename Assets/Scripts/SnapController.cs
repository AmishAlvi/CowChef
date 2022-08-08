using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    private List<Transform> snapPoints;
    private List<Draggable> draggableObjects;

    public float snapRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        snapPoints = new List<Transform>();
        draggableObjects = new List<Draggable>();

        foreach (Draggable dg in FindObjectsOfType(typeof(Draggable)))
        {
            draggableObjects.Add(dg);
        }

        foreach(GameObject gp in GameObject.FindGameObjectsWithTag("grid point"))
        {
            snapPoints.Add(gp.transform);
        }


        foreach(Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = onDragEnded;
        }
    }

   private void onDragEnded(Draggable draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach(Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition);
            if(closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if(closestSnapPoint != null && closestDistance <= snapRange)
        {
            draggable.transform.localPosition = closestSnapPoint.localPosition;
        }
        else
        {
            draggable.transform.localPosition = new Vector3(draggable.MouseDragStartPosition.x, draggable.MouseDragStartPosition.y, 0);
            onDragEnded(draggable);
        }
    }

    //private void Snap(float closestDistance, Transform closestSnapPoint,)
    //{

    //}
}

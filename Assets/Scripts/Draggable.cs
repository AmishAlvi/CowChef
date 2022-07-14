using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public delegate void DragEndedDelegate(Draggable draggable);

    public DragEndedDelegate dragEndedCallback;

    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition;
    private bool isDragged = false;
    private LineRenderer lineRenderer;
    bool lineRendererStatus;
    private RayCaster rayCaster;

    Camera cam;

    public Vector3 MouseDragStartPosition { get => mouseDragStartPosition; set => mouseDragStartPosition = value; }

    private void Awake()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        rayCaster = GetComponent<RayCaster>();
        lineRendererStatus = lineRenderer.enabled;
    }


    private void OnMouseDown()
    {
        isDragged = true;
        MouseDragStartPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPosition = transform.localPosition;
    }

    private void OnMouseDrag()
    {
        //lineRenderer.enabled = false;
        if(isDragged)
        {
            transform.localPosition = spriteDragStartPosition + (cam.ScreenToWorldPoint(Input.mousePosition) - MouseDragStartPosition);
        }
    }

    private void OnMouseUp()
    {
        isDragged = false;
        dragEndedCallback(this);
    }

    /*Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }*/
}

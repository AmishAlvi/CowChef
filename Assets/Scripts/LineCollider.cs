using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class LineCollider : MonoBehaviour
{
    EdgeCollider2D edgeCollider;
    LineRenderer lineRenderer;

    private void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        SetEdgeCollider(lineRenderer);
    }
    void SetEdgeCollider(LineRenderer lineRenderer)
    {
        List<Vector2> edges = new List<Vector2>();

        for(int point = 0; point < lineRenderer.positionCount; point++)
        {
            Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x - transform.position.x, lineRendererPoint.y - transform.position.y));
        }

        edgeCollider.SetPoints(edges);
        
    }
}

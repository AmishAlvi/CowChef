using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    public int reflections;
    public float maxLength;

    private LineRenderer lineRenderer;
    private Ray2D ray;
    private RaycastHit2D hit;
    public Vector2 direction;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray2D(transform.position, direction);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for(int i = 0; i< reflections; i++)
        {
            hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength);
            if (hit)
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector2.Distance(ray.origin, hit.point);
                //ray = new Ray2D(hit.point, Vector2.Reflect(ray.direction, hit.normal));
                //ray = new Ray2D(hit.point, transform.up);
                if (hit.collider.tag == "mirror")
                {
                    ray = new Ray2D(hit.point, Vector2.Reflect(ray.direction, hit.normal));
                    //break;
                }

                if (hit.collider.tag == "two way")
                {
                    Debug.Log("here " + remainingLength);
                    ray = new Ray2D(hit.point - new Vector2(maxLength/2, 0), transform.right);
                    //ray = new Ray2D(hit.point, -transform.right);
                }
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
}

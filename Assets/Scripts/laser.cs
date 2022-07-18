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
    private int mirrorsMask;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mirrorsMask = 1 << LayerMask.NameToLayer("Mirrors");
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray2D(transform.position, direction);
        Vector2 originalDirection = ray.direction;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;
        

        for(int i = 0; i< reflections; i++)
        {
            
            hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength, mirrorsMask);
            //Debug.Log(hit.point.normalized + " hit normal: " + hit.normal + " ray direction: " + ray.direction);
            if (hit)
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector2.Distance(ray.origin, hit.point);
                Vector2 reflectDirection;
                
                if (hit.collider.tag == "mirror")
                {
                    reflectDirection = Vector2.Reflect(ray.direction.normalized, hit.normal);
                   // Debug.Log("reflect direction: " + reflectDirection);
                }
                else
                {
                    reflectDirection = ray.direction;
                    if (hit.collider.tag == "food")
                    {
                        Food hitFood = hit.collider.gameObject.GetComponent<Food>();
                        hitFood.isAdded = true;
                    }
                    
                }

                ray = new Ray2D(hit.point + reflectDirection, reflectDirection);

            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }

       // Debug.Log("ray direction after: " + ray.direction);
        //Debug.Log("------");
    }
}

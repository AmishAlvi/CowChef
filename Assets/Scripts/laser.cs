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
    private Food hitFood;
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
            
            if (hit)
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector2.Distance(ray.origin, hit.point);
                Vector2 reflectDirection = GetDirection(hit, ray.direction, hit.collider.tag);
                
                /*if(hit.collider.tag == "food")
                {
                    hitFood = hit.collider.gameObject.GetComponent<Food>();
                    hitFood.isBeingAdded = true;
                }
                else
                {
                    if(hitFood != null)
                    {
                        hitFood.isBeingAdded=false;
                    }
                }*/

                ray = new Ray2D(hit.point + reflectDirection, reflectDirection);

            }
            else
            {
                lineRenderer.positionCount += 1;
                //lineRenderer.startColor = Color.blue;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }

    }

    private Vector2 GetDirection(RaycastHit2D hit, Vector2 rayDirection, string tag)
    {

        Vector2 direction = tag == "mirror" ? Vector2.Reflect(rayDirection.normalized, hit.normal) : rayDirection;
        if(Vector2.Dot(direction, rayDirection) == -1)
        {
            return Vector2.zero;
        }

        return direction;
    }

}

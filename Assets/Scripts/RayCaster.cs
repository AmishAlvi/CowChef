using UnityEngine;
using System.Collections.Generic;
public class RayCaster : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Vector2 direction;
    public float distance = 100;
    [HideInInspector]
    public Vector2 contactPoint = Vector2.zero;

    private int mirrorsMask;
    private RaycastHit2D mirrorsHit;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mirrorsMask = 1 << LayerMask.NameToLayer("Mirrors");
    }

    private void OnEnable()
    {
        lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
        if (mirrorsHit)
        {
            var raycaster = mirrorsHit.transform.GetComponent<RayCaster>();
            raycaster.enabled = false;
        }
    }

    void Update()
    {
        lineRenderer.SetPosition(0, contactPoint != Vector2.zero ? contactPoint : transform.position);
        lineRenderer.SetPosition(1, direction * distance);

        Vector2 position = (Vector2)transform.position + direction;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, mirrorsMask);
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
            mirrorsHit = hit;
            var raycaster = hit.transform.GetComponent<RayCaster>();
            Vector2 reflectDirection;
            if (hit.collider.tag == "mirror")
            {
                
                 reflectDirection = Vector2.Reflect(direction, hit.normal);
                
            }
            else
            {
                if(hit.collider.tag == "food")
                {
                    Food hitFood = hit.collider.gameObject.GetComponent<Food>();
                    hitFood.isAdded = true;
                }
                Debug.Log(hit.collider.tag);
                reflectDirection = direction;
            }

            Debug.Log("reflect direction: " + reflectDirection);
            raycaster.direction = reflectDirection;
            raycaster.contactPoint = hit.point;
            raycaster.enabled = true;
        }
        else if (mirrorsHit)
        {
            lineRenderer.SetPosition(1, direction * distance);
            
            var raycaster = mirrorsHit.transform.GetComponent<RayCaster>();
            raycaster.enabled = false;
        }
    }
}

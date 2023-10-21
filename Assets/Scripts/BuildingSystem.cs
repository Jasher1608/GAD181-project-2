using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingSystem
{
    public static Vector3 ClosestTile(Vector3 mousePos)
    {
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);
        
        float x = RoundToNearestQuarter(worldPos2D.x);
        float y = RoundToNearestQuarter(worldPos2D.y);
        float z = 0;
        return new Vector3(x, y, z);
    }

    private static float RoundToNearestQuarter(float value)
    {
        if (Mathf.Round(value) == (Mathf.Ceil(value)))
        {
            value = Mathf.Floor(value);
            return (value + 0.75f);
        }
        else if (Mathf.Round(value) == (Mathf.Floor(value)))
        {
            value = Mathf.Floor(value);
            return (value + 0.25f);
        }
        else
        {
            Debug.Log("else");
            return value;
        }
    }

    public static bool IsObjectHere(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;

        hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        if (hit != false)
        {
            if (hit.collider != null && hit.collider.CompareTag("Building"))
            {
                return true;
            }
            else
            {
                Debug.Log(hit.collider.gameObject.name);
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

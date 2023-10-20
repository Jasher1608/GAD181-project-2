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
        float z = worldPos.z;
        return new Vector3(x, y, z);
    }

    private static float RoundToNearestQuarter(float value)
    {
        if (Mathf.Approximately(value, Mathf.Round(value)))
        {
            return (value + 0.75f);
        }
        else
        {
            value = Mathf.Floor(value);
            return (value + 0.75f);
        }
    }
}

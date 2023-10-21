using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buildable Class", menuName = "Item/Buildable")]
public class BuildableClass : ItemClass
{
    public override BuildableClass GetBuildable() { return this; }

    public int size = 1;

    public GameObject prefabNorth;
    public GameObject prefabEast;
    public GameObject prefabSouth;
    public GameObject prefabWest;

    public override void Use(PlayerController caller)
    {
        base.Use(caller);
        
        switch (PlayerController.direction)
        {
            case BuildingDirection.North:
                Instantiate(prefabNorth, BuildingSystem.ClosestTile(Input.mousePosition), Quaternion.identity);
                break;
            case BuildingDirection.East:
                Instantiate(prefabEast, BuildingSystem.ClosestTile(Input.mousePosition), Quaternion.identity);
                break;
            case BuildingDirection.South:
                Instantiate(prefabSouth, BuildingSystem.ClosestTile(Input.mousePosition), Quaternion.identity);
                break;
            case BuildingDirection.West:
                Instantiate(prefabWest, BuildingSystem.ClosestTile(Input.mousePosition), Quaternion.identity);
                break;
        }
    }
}

public enum BuildingDirection
{
    North,
    East,
    South,
    West
}

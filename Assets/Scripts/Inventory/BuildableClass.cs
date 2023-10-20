using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buildable Class", menuName = "Item/Buildable")]
public class BuildableClass : ItemClass
{
    public override BuildableClass GetBuildable() { return this; }

    public GameObject prefab;

    public override void Use(PlayerController caller)
    {
        base.Use(caller);
        Instantiate(prefab, BuildingSystem.ClosestTile(Input.mousePosition), Quaternion.identity);
    }
}

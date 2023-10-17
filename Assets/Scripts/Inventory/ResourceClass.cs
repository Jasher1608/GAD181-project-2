using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Class", menuName = "Item/Resource")]
public class ResourceClass : ItemClass
{
    //[Header("Resource:")]
    public override ItemClass GetItem() { return this; }
    public override ResourceClass GetResource() { return this; }
}

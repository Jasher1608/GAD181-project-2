using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass : ScriptableObject
{
    [Header("Item:")]
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable = true;

    public virtual void Use(PlayerController caller)
    {
        Debug.Log("Used Item");
    }
    public virtual ItemClass GetItem() { return this; }
    public virtual ResourceClass GetResource() { return null; }
}

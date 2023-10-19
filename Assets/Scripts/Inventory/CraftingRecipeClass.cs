using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCraftingRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipeClass : ScriptableObject
{
    public SlotClass[] inputItems;
    public SlotClass outputItems;
    public float craftingTime;

    public bool CanCraft(InventoryManger inventory)
    {
        // Check if there is space in inventory
        if (inventory.isFull())
        {
            return false;
        }
        
        for (int i = 0; i < inputItems.Length; i++)
        {
            if (!inventory.Contains(inputItems[i].GetItem(), inputItems[i].GetQuantity()))
            {
                return false;
            }
        }
        
        return true;
    }

    public void Craft(InventoryManger inventory)
    {
        // Remove input items from inventory
        for (int i = 0; i < inputItems.Length; i++)
        {
            inventory.Remove(inputItems[i].GetItem(), inputItems[i].GetQuantity());
        }
        // Add the output item to the inventory
        inventory.Add(outputItems.GetItem(), outputItems.GetQuantity());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManger : MonoBehaviour
{
    [SerializeField] private GameObject slotHolder;
    
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;

    public List<ItemClass> items = new List<ItemClass>();

    private GameObject[] slots;

    public void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        
        // Set all the slots
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        RefreshUI();
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemIcon;
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }

    public void Add(ItemClass item)
    {
        items.Add(item);
        RefreshUI();
    }

    public void Remove(ItemClass item)
    {
        items.Remove(item);
        RefreshUI();
    }
}

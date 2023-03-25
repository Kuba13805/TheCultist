using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] InventorySlots;
    public GameObject inventoryItemPrefab;

    public bool AddItem(BaseItem item)
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItemDragDrop itemInSlot = slot.GetComponentInChildren<InventoryItemDragDrop>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(BaseItem item, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItemDragDrop inventoryItemDragDrop = newItemGameObject.GetComponent<InventoryItemDragDrop>();
        inventoryItemDragDrop.InitialiseItem(item);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : InventorySlot
{
    public partsOfArmorToEquipt armorPartToEquipt;

    private void Update()
    {
        
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItemDragDrop inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemDragDrop>();
            if ((inventoryItem.item.itemType.ToString() == "Armor")&& (inventoryItem.item.armorPart.ToString() == armorPartToEquipt.ToString()))
            {
                inventoryItem.parentAfterDrag = transform;
            }
        }
    }
    public enum partsOfArmorToEquipt
    {
        Head,
        Chest,
        Pants,
        Gloves,
        Ring
    }
}

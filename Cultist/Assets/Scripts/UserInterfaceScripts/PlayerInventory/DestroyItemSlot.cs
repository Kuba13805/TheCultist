using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyItemSlot : InventorySlot
{
    public override void OnDrop(PointerEventData eventData)
    {
        InventoryItemDragDrop inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemDragDrop>();
        inventoryItem.parentAfterDrag = transform;
        if (transform.childCount == 0)
        {
            Destroy(eventData.pointerDrag.GetComponent<InventoryItemDragDrop>());
        }
    }
}

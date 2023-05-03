using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0) return;
        
        var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemDragDrop>();
        inventoryItem.parentAfterDrag = transform;
    }
}

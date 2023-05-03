using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EquipmentSlot : InventorySlot
{
    public GameObject inventoryItemPrefab;
    public PartsOfArmorToEquip armorPartToEquip;

    private BaseItem _itemEquipped;
    
    public enum PartsOfArmorToEquip
    {
        Head,
        Chest,
        Pants,
        Gloves,
        Ring
    }

    public void OnEnable()
    {
        SpawnEquippedItem();
    }

    public void OnDisable()
    {
        ClearInventoryAfterReload();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0) return;
        
        var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemDragDrop>();
            
        if (inventoryItem.item.itemType != BaseItem.ItemTypes.Armor ||
            inventoryItem.item.armorPart.ToString() != armorPartToEquip.ToString()) return;
            
        inventoryItem.parentAfterDrag = transform;
    }

    private void SpawnEquippedItem()
    {
        if (GameManager.Instance.playerData == null) return;
        foreach (var item in GameManager.Instance.playerData.characterEquipment)
        {
            if (item.armorPart.ToString() != armorPartToEquip.ToString()) continue;
            
            var itemPrefabSpawn = Instantiate(inventoryItemPrefab, transform, false);
            itemPrefabSpawn.GetComponent<InventoryItemDragDrop>().item = item;
        }
    }

    private void ClearInventoryAfterReload()
    {
        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }
    }
}

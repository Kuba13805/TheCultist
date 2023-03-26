using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : InventorySlot
{
    public GameObject inventoryItemPrefab;
    public partsOfArmorToEquipt armorPartToEquipt;

    public enum partsOfArmorToEquipt
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
        InventoryItemDragDrop.OnItemChanged += ChangeStatValueOverItemChange;
    }

    public void OnDisable()
    {
        ClearInventoryAfterReload();
        InventoryItemDragDrop.OnItemChanged -= ChangeStatValueOverItemChange;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItemDragDrop inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemDragDrop>();
            if (inventoryItem.item.itemType.ToString() == "Armor" && inventoryItem.item.armorPart.ToString() == armorPartToEquipt.ToString())
            {
                inventoryItem.parentAfterDrag = transform;
            }
        }
    }

    public void SpawnEquippedItem()
    {
        foreach (var item in GameManager.Instance.PlayerData.characterEquipment)
        {
            if (item.armorPart.ToString() == armorPartToEquipt.ToString())
            {
                GameObject itemPrefabSpawn = Instantiate(inventoryItemPrefab, transform, false);
                itemPrefabSpawn.GetComponent<InventoryItemDragDrop>().item = item;
            }
        }
    }
    void ClearInventoryAfterReload()
    {
        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }
    }

    private void ChangeStatValueOverItemChange()
    {
        if (transform.childCount == 1)
        {
            OnNotEquipped();
        }
        else
        {
            OnEquipped();
        }
    }

    private void OnEquipped()
    {
        foreach (var t in GetComponentInChildren<InventoryItemDragDrop>().item.effectsOnItem)
        {
            t.isEffectActive = true;
            CalculateStatValue(t);
        }
    }

    private void OnNotEquipped()
    {
        foreach (var t in GetComponentInChildren<InventoryItemDragDrop>().item.effectsOnItem)
        {
            t.isEffectActive = false;
            CalculateStatValue(t);
        }
    }

    void CalculateStatValue(ItemEffect itemEffect)
    {
        itemEffect.AffectStat(GameManager.Instance.PlayerData);
    }
}

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

    private BaseItem itemEquipped;

    private List<ItemMultipleEffects> itemEffectsList;
    public enum partsOfArmorToEquipt
    {
        Head,
        Chest,
        Pants,
        Gloves,
        Ring
    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            itemEffectsList = null;
        }
    }

    public void OnEnable()
    {
        SpawnEquippedItem();
        InventoryItemDragDrop.OnItemEquipped += OnEquipped;
        InventoryItemDragDrop.OnItemStriped += OnNotEquipped;
    }

    public void OnDisable()
    {
        ClearInventoryAfterReload();
        InventoryItemDragDrop.OnItemEquipped -= OnEquipped;
        InventoryItemDragDrop.OnItemStriped -= OnNotEquipped;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItemDragDrop inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemDragDrop>();
            if (inventoryItem.item.itemType.ToString() == "Armor" && inventoryItem.item.armorPart.ToString() == armorPartToEquipt.ToString())
            {
                inventoryItem.parentAfterDrag = transform;
                itemEffectsList = inventoryItem.item.effectsOnItem;
            }
        }
    }
    public void SpawnEquippedItem()
    {
        if (GameManager.Instance.PlayerData == null) return;
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

    private void OnEquipped()
    {
        if (itemEffectsList == null || GetComponentInChildren<InventoryItemDragDrop>().effectsActive) return;
        foreach (var t in itemEffectsList)
        {
            foreach (var effect in t.listOfAdditionalEffects)
            {
                effect.isEffectActive = true;
                CalculateStatValue(effect);
            }
        }
        GetComponentInChildren<InventoryItemDragDrop>().effectsActive = true;
    }
    private void OnNotEquipped()
    {
        if (itemEffectsList == null || GetComponentInChildren<InventoryItemDragDrop>().effectsActive == false) return;
        foreach (var t in itemEffectsList)
        {
            foreach (var effect in t.listOfAdditionalEffects)
            {
                effect.isEffectActive = false;
                CalculateStatValue(effect);
            }
        }
        GetComponentInChildren<InventoryItemDragDrop>().effectsActive = false;
    }

    void CalculateStatValue(ItemEffect itemEffect)
    {
        itemEffect.AffectStat(GameManager.Instance.PlayerData);
    }
}

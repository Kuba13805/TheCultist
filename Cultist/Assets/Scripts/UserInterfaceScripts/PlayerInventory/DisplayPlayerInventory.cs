using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DisplayPlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject emptySlotPrefab;

    private void OnEnable()
    {
        InventoryItemDragDrop.OnItemChanged += ReloadInventory;
        DisplayInventory();
    }
    private void OnDisable()
    {
        InventoryItemDragDrop.OnItemChanged -= ReloadInventory;
        ClearInventoryAfterReload();
    }

    public void DisplayInventory()
    {
        foreach (var item in GameManager.Instance.playerData.playerInventoryItems)
        {
            SpawnNewItem(item);
        }
    }

    void SpawnNewItem(BaseItem item)
    {
        GameObject slotPrefabToSpawn = SpawnNewEmptySlot();
        GameObject itemPrefabSpawn = Instantiate(inventoryItemPrefab, slotPrefabToSpawn.transform, false);
        itemPrefabSpawn.GetComponent<InventoryItemDragDrop>().item = item;
        itemPrefabSpawn.GetComponent<InventoryItemDragDrop>().isInPlayerInventory = true;
    }
    GameObject SpawnNewEmptySlot()
    {
        GameObject slotPrefabToSpawn = Instantiate(emptySlotPrefab, transform, false);
        return slotPrefabToSpawn;
    }

    void ClearInventoryAfterReload()
    {
        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }
    }

    public void ReloadInventory()
    {
        var root = transform.root;
        root.gameObject.SetActive(false);
        root.gameObject.SetActive(true);
    }
}

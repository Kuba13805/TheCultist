using Managers;
using UnityEngine;

public class DisplayPlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject emptySlotPrefab;

    private void OnEnable()
    {
        InventoryItemDragDrop.OnItemAddedToInventory += ReloadInventory;
        
        InventoryItemDragDrop.OnItemRemovedFromInventory += ReloadInventory;
        
        InventoryItemDragDrop.OnItemEquipped += ReloadInventory;
        
        InventoryItemDragDrop.OnItemStriped += ReloadInventory;
        
        DisplayInventory();
    }
    private void OnDisable()
    {
        InventoryItemDragDrop.OnItemAddedToInventory -= ReloadInventory;
        
        InventoryItemDragDrop.OnItemRemovedFromInventory -= ReloadInventory;
        
        InventoryItemDragDrop.OnItemEquipped -= ReloadInventory;
        
        InventoryItemDragDrop.OnItemStriped -= ReloadInventory;
        
        ClearInventoryAfterReload();
    }

    private void DisplayInventory()
    {
        foreach (var item in GameManager.Instance.playerData.playerInventoryItems)
        {
            SpawnNewItem(item);
        }
    }

    private void SpawnNewItem(BaseItem item)
    {
        var slotPrefabToSpawn = SpawnNewEmptySlot();
        
        var itemPrefabSpawn = Instantiate(inventoryItemPrefab, slotPrefabToSpawn.transform, false);
        
        itemPrefabSpawn.GetComponent<InventoryItemDragDrop>().item = item;
        itemPrefabSpawn.GetComponent<InventoryItemDragDrop>().isInPlayerInventory = true;
    }

    private GameObject SpawnNewEmptySlot()
    {
        var slotPrefabToSpawn = Instantiate(emptySlotPrefab, transform, false);
        return slotPrefabToSpawn;
    }

    private void ClearInventoryAfterReload()
    {
        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }
    }

    private void ReloadInventory(BaseItem item)
    {
        ClearInventoryAfterReload();
        
        DisplayInventory();
    }
}

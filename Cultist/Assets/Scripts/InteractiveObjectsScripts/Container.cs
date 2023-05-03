using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class Container : BaseInteractableObject
{
    private enum ContainerType
    {
        Chest,
        Barrel,
        Basket
    }
    public GameObject inventoryItemPrefab;
    public GameObject emptySlotPrefab;

    [SerializeField] private ContainerType type;
    public List<BaseItem> itemsInContainer;
    
    private int containerSlotsNumber;
    private GameObject containerUIToLoad;
    private GameObject panelInstance;
    public bool isActive;
    public override void Interact()
    {
        InventoryItemDragDrop.OnItemAddedToInventory += RemoveItemFromContainerList;
        ShowContainerLoadout();
    }

    private void ShowContainerLoadout()
    {
        DeterminePanelToShow();
        
        if (Time.timeScale != 0)
        {
            if (isActive)
            {
                Destroy(panelInstance.gameObject);
            }
        }
        
        Canvas canvas = FindObjectOfType<Canvas>();
        
        panelInstance = Instantiate(containerUIToLoad, canvas.transform, false);
        
        isActive = true;

        FillLoadout(itemsInContainer.ToArray());

    }

    private void FillLoadout(BaseItem[] items)
    {
        foreach (var t in items)
        {
            SpawnNewItem(t);
        }

        for (int i = 0; i < containerSlotsNumber - items.Length; i++)
        {
            SpawnNewEmptySlot();
        }
    }

    private void SpawnNewItem(BaseItem item)
    {
        var slotPrefabToSpawn = SpawnNewEmptySlot();
        
        var itemPrefabSpawn = Instantiate(inventoryItemPrefab, slotPrefabToSpawn.transform, false);

        itemPrefabSpawn.GetComponent<InventoryItemDragDrop>().item = item;
        
    }

    private GameObject SpawnNewEmptySlot()
    {
        var content = panelInstance.GetComponentInChildren<GridLayoutGroup>().gameObject;
        
        var slotPrefabToSpawn = Instantiate(emptySlotPrefab, content.transform, false);
        
        return slotPrefabToSpawn;
    }
    private void DeterminePanelToShow()
    {
        switch (type)
        {
            case ContainerType.Chest:
            {
                containerUIToLoad = Resources.Load<GameObject>("ChestContainerInventory");
                containerSlotsNumber = 15;
                break;
            }
            case ContainerType.Barrel:
            {
                containerUIToLoad = Resources.Load<GameObject>("BarrelContainerInventory");
                containerSlotsNumber = 6;
                break;
            }
            case ContainerType.Basket:
            {
                containerUIToLoad = Resources.Load<GameObject>("BasketContainerInventory");
                containerSlotsNumber = 3;
                break;
            }
        }
    }

    private void RemoveItemFromContainerList(BaseItem item)
    {
        itemsInContainer.Remove(item);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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
    private InventorySlot[] containerSlots;
    public List<BaseItem> itemsInContainer;
    
    private int containerSlotsNumber;
    private GameObject containerUIToLoad;
    private GameObject panelInstance;
    public bool isActive;
    public int emptySlotsLeft;
    public override void Interact()
    {
        InventoryItemDragDrop.OnItemAddedFromContainer += RemoveItemFromContainerList;
        ShowContainerLoadout();
        PauseGame();
    }

    private void ShowContainerLoadout()
    {
        DeterminePanelToShow();
        
        containerSlots = containerUIToLoad.GetComponentInChildren<GridLayoutGroup>().GetComponentsInChildren<InventorySlot>();
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
        for (int i = 0; i < items.Length; i++)
        {
            SpawnNewItem(items[i]);
        }
        for (int i = 0; i < containerSlotsNumber - items.Length; i++)
        {
            SpawnNewEmptySlot();
        }
    }
    void SpawnNewItem(BaseItem item)
    {
        GameObject slotPrefabToSpawn = SpawnNewEmptySlot();
        GameObject itemPrefabSpawn = Instantiate(inventoryItemPrefab, slotPrefabToSpawn.transform, false);

        itemPrefabSpawn.GetComponent<InventoryItemDragDrop>().item = item;
        
    }

    GameObject SpawnNewEmptySlot()
    {
        GameObject content = panelInstance.GetComponentInChildren<GridLayoutGroup>().gameObject;
        GameObject slotPrefabToSpawn = Instantiate(emptySlotPrefab, content.transform, false);
        emptySlotsLeft++;
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
    void RemoveItemFromContainerList(BaseItem item)
    {
        itemsInContainer.Remove(item);
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }
}

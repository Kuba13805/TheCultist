using System.Collections.Generic;
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

    private int containerSlots;
    public GameObject inventoryItemPrefab;
    public GameObject emptySlotPrefab;
    private GameObject containerUIToLoad;
    private GameObject panelInstance;
    [SerializeField] private ContainerType type;
    public InventorySlot[] containerEmptySlots;
    public BaseItem[] itemsInContainer;
    public bool isActive;
    public override void Interact()
    {
        ShowContainerLoadout();
        PauseGame();
    }

    private void ShowContainerLoadout()
    {
        DeterminePanelToShow();
        
        containerEmptySlots = containerUIToLoad.GetComponentInChildren<GridLayoutGroup>().GetComponentsInChildren<InventorySlot>();
        
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
        
        FillLoadout(itemsInContainer);
    }

    private void FillLoadout(BaseItem[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log("Spawning on: " + i);
            SpawnNewItem(items[i]);
        }
        for (int i = 0; i < containerSlots - items.Length; i++)
        {
            SpawnNewEmptySlot();
        }
    }
    void SpawnNewItem(BaseItem item)
    {
        GameObject slotPrefabToSpawn = Instantiate(emptySlotPrefab);
        GameObject itemPrefabSpawn = Instantiate(inventoryItemPrefab);
        GameObject content = panelInstance.GetComponentInChildren<GridLayoutGroup>().gameObject;
        slotPrefabToSpawn.transform.SetParent(content.transform);
        itemPrefabSpawn.transform.SetParent(slotPrefabToSpawn.transform);
        itemPrefabSpawn.GetComponent<InventoryItemDragDrop>().item = item;
        Debug.Log("Item spawned: " + item.itemName);
    }

    void SpawnNewEmptySlot()
    {
        GameObject slotPrefabToSpawn = Instantiate(emptySlotPrefab);
        GameObject content = panelInstance.GetComponentInChildren<GridLayoutGroup>().gameObject;
        slotPrefabToSpawn.transform.SetParent(content.transform);
    }
    private void DeterminePanelToShow()
    {
        switch (type)
        {
            case ContainerType.Chest:
            {
                containerUIToLoad = Resources.Load<GameObject>("ChestContainerInventory");
                containerSlots = 15;
                break;
            }
            case ContainerType.Barrel:
            {
                containerUIToLoad = Resources.Load<GameObject>("BarrelContainerInventory");
                containerSlots = 6;
                break;
            }
            case ContainerType.Basket:
            {
                containerUIToLoad = Resources.Load<GameObject>("BasketContainerInventory");
                containerSlots = 3;
                break;
            }
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }
}

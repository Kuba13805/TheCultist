using System.Collections.Generic;
using UnityEngine;

public class Container : BaseInteractableObject
{
    private enum ContainerType
    {
        Chest,
        Barrel,
        Basket
    }

    private int emptySlotsInContainer;
    public GameObject inventoryItemPrefab;
    private GameObject containerUIToLoad;
    private GameObject panelInstance;
    [SerializeField] private ContainerType type;
    public InventorySlot[] containerLoadout;
    public bool isActive;
    public override void Interact()
    {
        ShowContainerLoadout();
        PauseGame();
    }

    private void ShowContainerLoadout()
    {
        DeterminePanelToShow(emptySlotsInContainer);
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
    }

    private void FillLoadout(BaseItem item)
    {
        for (int i = 0; i < emptySlotsInContainer; i++)
        {
            InventorySlot slot = containerLoadout[i];
            InventoryItemDragDrop itemInSlot = slot.GetComponentInChildren<InventoryItemDragDrop>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }
    void SpawnNewItem(BaseItem item, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItemDragDrop inventoryItemDragDrop = newItemGameObject.GetComponent<InventoryItemDragDrop>();
        inventoryItemDragDrop.InitialiseItem(item);
    }
    private void DeterminePanelToShow(int slotsToLoad)
    {
        switch (type)
        {
            case ContainerType.Chest:
            {
                containerUIToLoad = Resources.Load<GameObject>("ChestContainerInventory");
                slotsToLoad = 15;
                break;
            }
            case ContainerType.Barrel:
            {
                containerUIToLoad = Resources.Load<GameObject>("BarrelContainerInventory");
                slotsToLoad = 6;
                break;
            }
            case ContainerType.Basket:
            {
                containerUIToLoad = Resources.Load<GameObject>("BasketContainerInventory");
                slotsToLoad = 3;
                break;
            }
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }
}

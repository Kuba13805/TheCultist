using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerExitHandler
{
    public bool isInPlayerInventory;
    public BaseItem item;
    public Button button;
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform parentBeforeDrag;

    public delegate void onItemChanged();

    public static event onItemChanged OnItemChanged;
    
    public delegate void onItemAddedFromContainer(BaseItem item);

    public static event onItemAddedFromContainer OnItemAddedFromContainer;

    private void Start()
    {
        InitialiseItem(item);
    }

    public void InitialiseItem(BaseItem newItem)
    {
        GetComponentInChildren<LoadItemIcon>().LoadIcon();
        GetComponentInChildren<TextMeshProUGUI>().text = newItem.itemName;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        var transform1 = transform;
        var parent = transform1.parent;
        parentBeforeDrag = parent;
        image.raycastTarget = false;
        parentAfterDrag = parent;
        transform.SetParent(transform1.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        if (parentBeforeDrag.GetComponent<InventorySlot>() && parentAfterDrag.GetComponent<EquipmentSlot>())
        {
            isInPlayerInventory = false;
            GameManager.Instance.PlayerData.playerInventoryItems.Remove(item);
            GameManager.Instance.PlayerData.characterEquipment.Add(item);
            OnItemChanged?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && isInPlayerInventory && transform.GetComponentInParent<InventorySlot>())
        {
            button.gameObject.SetActive(true);
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left && !isInPlayerInventory && !transform.GetComponentInParent<EquipmentSlot>())
        {
            GameManager.Instance.PlayerData.playerInventoryItems.Add(item);
            OnItemAddedFromContainer?.Invoke(item);
            DestroyItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Left && transform.GetComponentInParent<EquipmentSlot>())
        {
            GameManager.Instance.PlayerData.playerInventoryItems.Add(item);
            GameManager.Instance.PlayerData.characterEquipment.Remove(item);
            DestroyItem();
            OnItemChanged?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.enabled)
        {
            button.gameObject.SetActive(false);
        }
    }
    
    public void DestroyItem()
    {
        if (isInPlayerInventory)
        {
            GameManager.Instance.PlayerData.playerInventoryItems.Remove(item);
        }
        Destroy(gameObject);
        OnItemChanged?.Invoke();
    }
}

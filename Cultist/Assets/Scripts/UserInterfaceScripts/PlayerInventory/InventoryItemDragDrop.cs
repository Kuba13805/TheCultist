using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryItemDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    public bool isInPlayerInventory;
    public BaseItem item;
    public Button button;
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform parentBeforeDrag;
    public bool effectsActive;
    public bool detailsPanelActive;
    private GameObject detailsPanelInstance;

    public delegate void onItemChanged();

    public static event onItemChanged OnItemChanged;
    
    
    public delegate void onItemAddedFromContainer(BaseItem item);

    public static event onItemAddedFromContainer OnItemAddedFromContainer;
    
    public delegate void onItemEquipped();

    public static event onItemEquipped OnItemEquipped;
    
    public delegate void onItemStriped();

    public static event onItemStriped OnItemStriped;

    private void Awake()
    {
        InputManager.Instance.PlayerInputActions.UI.DoubleClick.performed += UseItem;
    }

    private void Start()
    {
        OnItemEquipped += SetEffectsActive;
        OnItemStriped += SetEffectsInactive;
        InitialiseItem(item);
        if (!isInPlayerInventory && GetComponentInParent<EquipmentSlot>())
        {
            effectsActive = true;
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.PlayerInputActions.UI.DoubleClick.performed -= UseItem;
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
            OnItemEquipped?.Invoke();
            OnItemChanged?.Invoke();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        var detailsPanelPrefab = Resources.Load<GameObject>("ItemDetailsPanel");
        if (detailsPanelActive) return;
        var uiPosition = Input.mousePosition;
        uiPosition.x = uiPosition.x + 120;
        uiPosition.y = uiPosition.y - 80;
        detailsPanelInstance = Instantiate(detailsPanelPrefab, uiPosition, Quaternion.identity, transform.root);
        detailsPanelActive = true;
        detailsPanelInstance.GetComponent<ItemDetailsPanelLoad>().LoadItemDetails(item);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && isInPlayerInventory && transform.GetComponentInParent<InventorySlot>() && !item.questItem)
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
            OnItemStriped?.Invoke();
            OnItemChanged?.Invoke();
        }
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.enabled)
        {
            button.gameObject.SetActive(false);
        }

        if (detailsPanelActive)
        {
            detailsPanelActive = false;
            Destroy(detailsPanelInstance.gameObject);
        }
    }
    public void DestroyItem()
    {
        if (isInPlayerInventory)
        {
            GameManager.Instance.PlayerData.playerInventoryItems.Remove(item);
        }

        if (detailsPanelActive)
        {
            Destroy(detailsPanelInstance.gameObject);
        }
        Destroy(gameObject);
        OnItemChanged?.Invoke();
        OnItemEquipped -= SetEffectsActive;
        OnItemStriped -= SetEffectsInactive;
    }

    private void SetEffectsActive()
    {
        effectsActive = true;
    }

    private void SetEffectsInactive()
    {
        effectsActive = false;
    }

    private void UseItem(InputAction.CallbackContext context)
    {
        if (!item.oneTimeItem) return;
        Debug.Log("Item has been used");
        DestroyItem();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryItemDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    public bool isInPlayerInventory;
    public BaseItem item;
    public Button button;
    public Image image;
    
    [HideInInspector] public Transform parentAfterDrag;
    private Transform _parentBeforeDrag;
    
    public bool effectsActive;
    public bool detailsPanelActive;
    
    private GameObject _detailsPanelInstance;

    #region Events

    public static event Action<BaseItem> OnItemEquipped;

    public static event Action<BaseItem> OnItemStriped;
    

    #endregion
    private void Start()
    {

        InitialiseItem(item);
        
        if (!isInPlayerInventory && GetComponentInParent<EquipmentSlot>())
        {
            effectsActive = true;
        }
    }
    private void InitialiseItem(BaseItem newItem)
    {
        GetComponentInChildren<LoadItemIcon>().LoadIcon();
        GetComponentInChildren<TextMeshProUGUI>().text = newItem.itemName;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        var transform1 = transform;
        var parent = transform1.parent;
        _parentBeforeDrag = parent;
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
        
        if (!_parentBeforeDrag.GetComponent<InventorySlot>() || !parentAfterDrag.GetComponent<EquipmentSlot>()) return;
        
        isInPlayerInventory = false;
        effectsActive = true;
        
        OnItemEquipped?.Invoke(item);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        InputManager.Instance.PlayerInputActions.UI.DoubleClick.performed += UseItem;

        DisplayItemDetailsPanel();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Right when isInPlayerInventory && transform.GetComponentInParent<InventorySlot>() && !item.questItem:
                button.gameObject.SetActive(true);
                break;
            
            case PointerEventData.InputButton.Left when !isInPlayerInventory && !transform.GetComponentInParent<EquipmentSlot>():
                var playerEvent = new PlayerEvents();
                PlayerEvents.AddItem(item);
                DestroyItem();
                break;
            
            case PointerEventData.InputButton.Left when transform.GetComponentInParent<EquipmentSlot>():
                effectsActive = false;
                OnItemStriped?.Invoke(item);
                DestroyItem();
                break;
        }
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        InputManager.Instance.PlayerInputActions.UI.DoubleClick.performed -= UseItem;
        
        if (button.enabled)
        {
            button.gameObject.SetActive(false);
        }

        if (!detailsPanelActive) return;
        
        detailsPanelActive = false;
        Destroy(_detailsPanelInstance.gameObject);
    }
    public void DestroyItem()
    {
        if (isInPlayerInventory)
        {
            var playerEvent = new PlayerEvents();
            PlayerEvents.RemoveItem(item);
        }
        
        if (detailsPanelActive)
        {
            Destroy(_detailsPanelInstance.gameObject);
        }

        Destroy(gameObject);
    }
    private void DisplayItemDetailsPanel()
    {
        var detailsPanelPrefab = Resources.Load<GameObject>("ItemDetailsPanel");
        if (detailsPanelActive) return;
        var uiPosition = Input.mousePosition;

        uiPosition.x += 120;
        uiPosition.y -= 80;

        _detailsPanelInstance = Instantiate(detailsPanelPrefab, uiPosition, Quaternion.identity, transform.root);
        detailsPanelActive = true;
        _detailsPanelInstance.GetComponent<ItemDetailsPanelLoad>().LoadItemDetails(item);
    }
    private void UseItem(InputAction.CallbackContext context)
    {
        if (!item.oneTimeItem || !isInPlayerInventory) return;
        Debug.Log("Item has been used");
        DestroyItem();
    }

    private void OnDestroy()
    {
        InputManager.Instance.PlayerInputActions.UI.DoubleClick.performed -= UseItem;
    }
}

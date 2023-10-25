using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DisplayItemDetails : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public BaseItem item;
    
    private bool _detailsPanelActive;
    private GameObject _detailsPanelInstance;

    private void Start()
    {
        GetComponent<Image>().sprite = item.icon;
    }

    private void DisplayItemDetailsPanel()
    {
        var detailsPanelPrefab = Resources.Load<GameObject>("ItemDetailsPanel");
        if (_detailsPanelActive) return;
        var uiPosition = transform.position;

        uiPosition.x += 160;
        if (uiPosition.x > 1800)
        {
            uiPosition.x -= 320;
        }
        uiPosition.y -= 80;

        _detailsPanelInstance = Instantiate(detailsPanelPrefab, uiPosition, Quaternion.identity, transform.root);
        _detailsPanelActive = true;
        _detailsPanelInstance.GetComponent<ItemDetailsPanelLoad>().LoadItemDetails(item);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayItemDetailsPanel();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(_detailsPanelInstance.gameObject);
        _detailsPanelActive = false;
    }

}

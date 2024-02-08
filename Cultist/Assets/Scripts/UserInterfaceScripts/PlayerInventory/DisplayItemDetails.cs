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
        uiPosition.y -= 80;

        _detailsPanelInstance = Instantiate(detailsPanelPrefab, uiPosition, Quaternion.identity, transform.root);
        
        Vector3[] v = new Vector3[4];
        
        _detailsPanelInstance.GetComponent<RectTransform>().GetWorldCorners(v);

        if (v[3].x > Screen.width)
        {
            Destroy(_detailsPanelInstance.gameObject);
            
            uiPosition.x -= 320;
            
            _detailsPanelInstance = Instantiate(detailsPanelPrefab, uiPosition, Quaternion.identity, transform.root);
        }
        Debug.Log("Not at screen" + v[0] + ";" + v[1] + ";" + v[2] + ";" + v[3] + ";");
        if (v[1].y < 90)
        {
            Destroy(_detailsPanelInstance.gameObject);
            
            uiPosition.y += 160;
            
            _detailsPanelInstance = Instantiate(detailsPanelPrefab, uiPosition, Quaternion.identity, transform.root);
        }
        
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

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowStatDesc : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject descPanel;
    
    [HideInInspector] public string desc;

    private GameObject panelInstance;

    public void OnPointerEnter(PointerEventData eventData)
    {
        panelInstance = Instantiate(descPanel,new Vector3(transform.position.x + 150, transform.position.y, 0) , Quaternion.identity, transform.root);

        panelInstance.GetComponentInChildren<TextMeshProUGUI>().text = desc;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(panelInstance.gameObject);
    }
}
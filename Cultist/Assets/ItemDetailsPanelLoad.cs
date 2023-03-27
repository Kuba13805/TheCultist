using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsPanelLoad : MonoBehaviour
{

    public void LoadItemDetails(BaseItem item)
    {
        transform.Find("ItemDesc").GetComponent<TextMeshProUGUI>().text = item.itemDesc;
        
        var itemDisplay = transform.Find("ItemDisplay");
        itemDisplay.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
        itemDisplay.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;

        transform.Find("ItemValueDisplay").GetComponentInChildren<TextMeshProUGUI>().text = item.value.ToString();

    }
}

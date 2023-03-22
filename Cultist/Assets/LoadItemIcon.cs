using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadItemIcon : MonoBehaviour
{
    public void LoadIcon()
    {
        GetComponent<Image>().sprite = GetComponentInParent<InventoryItemDragDrop>().item.icon;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem : MonoBehaviour
{
    public string itemName;
    [SerializeField] private int itemId;
    public Text itemDesc;
    [SerializeField] private bool oneTimeItem;
    public int price;
    [SerializeField] private bool questItem;

}

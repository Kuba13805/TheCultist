using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item/Create New Item")]
public class BaseItem : ScriptableObject
{
    public string itemName;
    public int itemId;
    [TextAreaAttribute(15, 20)]
    public string itemDesc;
    [SerializeField] private bool oneTimeItem;
    public int value;
    [SerializeField] private bool questItem;
    public Sprite icon;

    public ItemTypes itemType;
    public enum ItemTypes
    {
        Armor,
        Tool,
        Food,
        Junk
    }

    public enum ArmorParts
    {
        Head,
        Chest,
        Pants,
        Gloves,
        Ring
    }
    [ShowIf("itemType", ItemTypes.Armor)] public ArmorParts armorPart;
}

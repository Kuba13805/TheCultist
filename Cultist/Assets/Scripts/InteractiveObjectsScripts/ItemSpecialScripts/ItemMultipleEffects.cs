using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMultipleEffectItem", menuName = "ScriptableObjects/ItemEffects/Create New Multiple Effect Item", order = 2)]
public class ItemMultipleEffects : ScriptableObject
{
    public string effectName;
    [SerializeField] private int effectId;
    
    public List<ItemEffect> listOfAdditionalEffects;
    
}

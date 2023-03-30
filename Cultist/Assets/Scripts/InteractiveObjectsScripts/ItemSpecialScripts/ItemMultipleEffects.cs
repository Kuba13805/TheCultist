using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMultipleEffectItem", menuName = "ScriptableObjects/ItemEffects/Create New Multiple Effect Item", order = 2)]
public class ItemMultipleEffects : ItemEffect
{
    
    public List<ItemEffect> listOfAdditionalEffects;
    
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        transform.Find("ItemEffectsDisplay").GetComponentInChildren<TextMeshProUGUI>().text = CreateTextForItemEffects(item.effectsOnItem);
    }

    private string CreateTextForItemEffects(List<ItemEffect> effectsList)
    {
        var textToReturn = "";
        textToReturn = effectsList.Count == 0 ? "This item has no special effects." : effectsList.Aggregate(textToReturn, (current, effect) => current + DetermineText(effect));
        return textToReturn;
    }

    private string DetermineText(ItemEffect effect)
    {
        string textToReturn = "";
        switch (effect.typeOfInfluence)
        {
            case ItemEffect.typesOfInfluenceOnStat.IncreaseStat:
                textToReturn =  effect.statToEffect + " is increased by " + effect.pointsAffecting + ".\n";
                break;
            case ItemEffect.typesOfInfluenceOnStat.DecreaseStat:
                textToReturn =  effect.statToEffect + " is decreased by " + effect.pointsAffecting + ".\n";
                break;
        }
        return textToReturn;
    }
}

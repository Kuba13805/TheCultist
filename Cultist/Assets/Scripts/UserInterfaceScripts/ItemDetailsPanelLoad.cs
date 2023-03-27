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
        string points = effect.pointsAffecting.ToString();
        if (effect.timeEffect)
        {
            string time;
            switch (effect.typeOfInfluence)
            {
                case ItemEffect.typesOfInfluenceOnStat.IncreaseStat:
                    points = $"<color=green>{points}</color>";
                    if (effect.effectTime > 60f)
                    {
                        time = $"<color=yellow>{effect.effectTime/60f}</color>";
                        textToReturn =  effect.statToEffect + $" is <color=green>increased</color> by " + points + " for " + time + " minutes.\n";
                    }
                    else
                    {
                        time = $"<color=yellow>{effect.effectTime}</color>";
                        textToReturn =  effect.statToEffect + $" is <color=green>increased</color> by " + points + " for " + time + " seconds.\n";
                    }
                    break;
                case ItemEffect.typesOfInfluenceOnStat.DecreaseStat:
                    points = $"<color=red>{points}</color>";
                    if (effect.effectTime > 60f)
                    {
                        time = $"<color=yellow>{effect.effectTime/60f}</color>";
                        textToReturn =  effect.statToEffect + $" is <color=green>decreased</color> by " + points + " for " + time + " minutes.\n";
                    }
                    else
                    {
                        time = $"<color=yellow>{effect.effectTime}</color>";
                        textToReturn =  effect.statToEffect + $" is <color=green>decreased</color> by " + points + " for " + time + " seconds.\n";
                    }
                    break;
            }
        }
        else
        {
            switch (effect.typeOfInfluence)
            {
                case ItemEffect.typesOfInfluenceOnStat.IncreaseStat:
                    points = $"<color=green>{points}</color>";
                    textToReturn =  effect.statToEffect + $" is <color=green>increased</color> by " + points + ".\n";
                    break;
                case ItemEffect.typesOfInfluenceOnStat.DecreaseStat:
                    points = $"<color=red>{points}</color>";
                    textToReturn =  effect.statToEffect + $" is <color=red>decreased</color> by " + points + ".\n";
                    break;
            }
        }
        
        return textToReturn;
    }
}

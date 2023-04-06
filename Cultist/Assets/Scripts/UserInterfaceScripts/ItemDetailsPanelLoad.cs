using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsPanelLoad : MonoBehaviour
{
    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public void LoadItemDetails(BaseItem item)
    {
        transform.Find("ItemDesc").GetComponent<TextMeshProUGUI>().text = item.itemDesc;

        var itemDisplay = transform.Find("ItemDisplay");
        itemDisplay.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
        itemDisplay.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;

        transform.Find("ItemValueDisplay").GetComponentInChildren<TextMeshProUGUI>().text = item.value.ToString();
        
        transform.Find("ItemEffectsDisplay").GetComponentInChildren<TextMeshProUGUI>().text = CreateTextForItemEffects(item.effectsOnItem);
    }

    private string CreateTextForItemEffects(List<ItemMultipleEffects> multipleEffectsList)
    {
        var textToReturn = "";
        if (multipleEffectsList.Count == 0)
        {
            return textToReturn = "This item has no special effects.";
        }

        textToReturn = "";
        foreach (var t in multipleEffectsList)
        {
            textToReturn += $"<b><size=14>{t.effectName}</size></b> \n";
            foreach (var effect in t.listOfAdditionalEffects)
            {
                textToReturn += DetermineText(effect);
            }
        }
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

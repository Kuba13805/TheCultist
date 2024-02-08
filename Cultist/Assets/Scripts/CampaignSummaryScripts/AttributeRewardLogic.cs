using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class AttributeRewardLogic : MonoBehaviour
{
    [SerializeField] private Image attributeIcon;

    [SerializeField] private TextMeshProUGUI attributeName;

    public void InitializeReward(string stat, int pointsToAdd)
    {
        attributeName.text = stat + " +"+ pointsToAdd;

        attributeIcon.sprite = Resources.Load<Sprite>("Sprites/stat" + stat + "Icon");
    }
}

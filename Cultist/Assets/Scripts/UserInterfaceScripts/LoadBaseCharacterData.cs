using System;
using Managers;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class LoadBaseCharacterData : MonoBehaviour
{
    [SerializeField] private Image portraitSprite;
    
    [SerializeField] private TextMeshProUGUI characterHealth;

    private void Start()
    {
        portraitSprite.sprite = GameManager.Instance.playerData.playerPortrait;

        characterHealth.text = GameManager.Instance.playerData.health.ToString();

        PlayerEvents.OnDamagePlayer += ModifyPlayerHealthDisplay;

        PlayerEvents.OnHealPlayer += ModifyPlayerHealthDisplay;
    }

    private void OnDisable()
    {
        PlayerEvents.OnDamagePlayer -= ModifyPlayerHealthDisplay;

        PlayerEvents.OnHealPlayer -= ModifyPlayerHealthDisplay;
    }

    private void ModifyPlayerHealthDisplay(int obj)
    {
        characterHealth.text = GameManager.Instance.playerData.health.ToString();
    }
}

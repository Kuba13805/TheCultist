using System;
using System.Linq;
using System.Reflection;
using PlayerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DisplayPlayerSkills : MonoBehaviour
{
    [SerializeField] private GameObject skillPrefab;

    [SerializeField] private PlayerData playerData;

    private void OnEnable()
    {
        DisplayAllSkills();
    }

    private void DisplayAllSkills()
    {
        FieldInfo[] baseStatFields = playerData.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Where(field => typeof(BaseStat).IsAssignableFrom(field.FieldType) &&
                            field.FieldType != typeof(Wisdom) && field.FieldType != typeof(Dexterity) 
                            && field.FieldType != typeof(Strength) && field.FieldType != typeof(Condition) 
                            && field.FieldType != typeof(Power))
            .ToArray();

        foreach (var skill in baseStatFields)
        {
            var skillPrompt = Instantiate(skillPrefab, transform);

            var skillName = skill.Name;
            
            skillPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = char.ToUpper(skillName[0]) + skillName[1..];

            BaseStat fieldValue = (BaseStat)skill.GetValue(playerData);
            
            skillPrompt.GetComponentsInChildren<TextMeshProUGUI>()[1].text = fieldValue.statValue.ToString();
        }
    }

    private void OnDisable()
    {
        foreach (var prompt in GetComponentsInChildren<HorizontalLayoutGroup>())
        {
            Destroy(prompt.gameObject);
        }
    }
}

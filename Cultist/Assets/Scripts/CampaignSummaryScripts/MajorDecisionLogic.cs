using System;
using Questlines.SingleQuests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MajorDecisionLogic : MonoBehaviour
{
    public QuestVariables currentQuestVariable;

    public static event Action<QuestVariables> OnQuestVariableClick;

    private void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = currentQuestVariable.variableName;

        if (currentQuestVariable.variableIcon == null) return;
        GetComponentInChildren<Button>().GetComponent<Image>().sprite = currentQuestVariable.variableIcon;
    }

    public void PassQuestDesc()
    {
        OnQuestVariableClick?.Invoke(currentQuestVariable);
    }
}

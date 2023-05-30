using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayedQuestline : MonoBehaviour
{
    public Questline questlineToDisplay;

    public static event Action<Questline> OnQuestlineButtonClicked;

    public void InvokeQuestline()
    {
        OnQuestlineButtonClicked?.Invoke(questlineToDisplay);
    }

    private void Start()
    {
        GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text =
            questlineToDisplay.questlineName;
    }
}

using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCampaign", menuName = "ScriptableObjects/Create New Campaign")]
public class Campaign : ScriptableObject
{
    [SerializeField] private int campaignId;

    [TextArea(2, 4)]
    public string campaignName;

    public Image campaignImage;

    [TextArea(15, 20)]
    public string campaignDesc;
    
    [SerializeField][Label("Questlines Required To Complete")] private List<Questline> requiredQuestlines;
    
    // startEvent

    [SerializeField][Label("Campaigns Required To Start")] private List<Campaign> requiredCampaigns;

    private bool hasStarted;

    private bool isCompleted;

    private void StartCampaign()
    {
        hasStarted = true;
    }

    private void CompleteCampaign()
    {
        isCompleted = true;
    }
}

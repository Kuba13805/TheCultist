using NaughtyAttributes;
using PlayerScripts;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayableCharacter", menuName = "ScriptableObjects/Create New Playable Character")]
public class PlayableCharacter : PlayerData
{
    protected override void OnEnable()
    {
        
    }

    [HorizontalLine(color: EColor.Green)]
    public Sprite characterIcon;

    [TextArea(15, 20)]
    public string charDesc;
    
    [TextArea(3, 5)]
    public string charQuote;

    public Campaign characterStartingCampaign;
}

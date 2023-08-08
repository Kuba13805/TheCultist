using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayableCharacter", menuName = "ScriptableObjects/Create New Playable Character")]
public class PlayableCharacter : PlayerData
{
    protected override void OnEnable()
    {
        
    }

    public Sprite characterIcon;

    [TextArea(15, 20)]
    public string charDesc;
    
    [TextArea(3, 5)]
    public string charQuote;
}

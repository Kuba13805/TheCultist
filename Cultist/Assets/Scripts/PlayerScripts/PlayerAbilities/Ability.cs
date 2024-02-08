using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "ScriptableObjects/Create New Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;

    public Sprite abilityIcon;

    [TextArea(3, 5)] public string abilityDesc;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string abilityName;

    public Sprite abilityIcon;

    [TextArea(3, 5)] public string abilityDesc;
}

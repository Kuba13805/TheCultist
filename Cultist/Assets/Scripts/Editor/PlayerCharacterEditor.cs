#if UNITY_EDITOR
using PlayerScripts;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerData), true)]
public class PlayerCharacterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!GUILayout.Button("Update")) return;
        
        var playerCharacter = (PlayerData)target;
        UpdateStat(playerCharacter);
    }

    private void UpdateStat(BaseCharacter playerCharacter)
    {
        UpdateStatDetails("Agility allows you to move faster and more nimbly around the city. In combat, " +
                          "it allows you to dodge enemy attacks and affects the player character's accuracy.", playerCharacter.dexterity, Resources.Load<Sprite>("Sprites/statDexterityIcon"));
        
        UpdateStatDetails("Strength determines your physical tetany. It allows you to " +
                          "lift heavy objects, hit harder during a fight or be a harder target to defeat.", playerCharacter.strength, Resources.Load<Sprite>("Sprites/statStrengthIcon"));
        
        UpdateStatDetails("Condition determines your resilience to diseases and injuries sustained." +
                          " Increases your life points and stamina against negative effects during gameplay.", playerCharacter.condition, Resources.Load<Sprite>("Sprites/statConditionIcon"));
        
        UpdateStatDetails("Knowledge is the key to knowing the world." +
                          " It allows you to learn more about what surrounds you and about the people living in this world. Sometimes it uncovers unknown facts and stories.", playerCharacter.wisdom, Resources.Load<Sprite>("Sprites/statWisdomIcon"));
        
        UpdateStatDetails("The power allows you to better draw on demon magic, cast spells or interact with enchanted objects." +
                          " Those who have possessed enough of it often reach for more, giving away part of themselves.", playerCharacter.power, Resources.Load<Sprite>("Sprites/statPowerIcon"));


        
        EditorUtility.SetDirty(playerCharacter);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static void UpdateStatDetails(string desc, BaseStat stat, Sprite statIcon)
    {
        stat.statDesc = desc;
        
        stat.statIcon = statIcon;
    }
}
#endif
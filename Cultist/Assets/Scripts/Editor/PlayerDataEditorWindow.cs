#if UNITY_EDITOR
using PlayerScripts;
using UnityEditor;
using UnityEngine;

public class PlayerDataEditorWindow : EditorWindow
{
    [MenuItem("Tools/Update PlayerData")]
    public static void ShowWindow()
    {
        GetWindow<PlayerDataEditorWindow>("Update PlayerData");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Update All PlayerData"))
        {
            UpdateAllPlayerData();
        }
    }

    private void UpdateAllPlayerData()
    {
        PlayerData[] scriptableObjects = Resources.LoadAll<PlayerData>("PlayableCharactersData");
        foreach (PlayerData scriptableObject in scriptableObjects)
        {
            UpdateStat(scriptableObject);
            EditorUtility.SetDirty(scriptableObject);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("PlayerData updated for all objects.");
    }

    private void UpdateStat(PlayerData playerCharacter)
    {
        Debug.Log(playerCharacter.charName + " - updated!");
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
        
        UpdateStatDetails(" ", playerCharacter.perception, Resources.Load<Sprite>("Sprites/statPerceptionIcon"));
        
        UpdateStatDetails(" ", playerCharacter.occultism, Resources.Load<Sprite>("Sprites/statOccultismIcon"));
        
        UpdateStatDetails(" ", playerCharacter.medicine, Resources.Load<Sprite>("Sprites/statMedicineIcon"));
        
        UpdateStatDetails(" ", playerCharacter.electrics, Resources.Load<Sprite>("Sprites/statElectricsIcon"));
        
        UpdateStatDetails(" ", playerCharacter.history, Resources.Load<Sprite>("Sprites/statOccultismIcon"));
        
        UpdateStatDetails(" ", playerCharacter.persuasion, Resources.Load<Sprite>("Sprites/statPersuasionIcon"));
        
        UpdateStatDetails(" ", playerCharacter.intimidation, Resources.Load<Sprite>("Sprites/statIntimidationIcon"));
        
        UpdateStatDetails(" ", playerCharacter.locksmithing, Resources.Load<Sprite>("Sprites/statLocksmithingIcon"));
        
        UpdateStatDetails(" ", playerCharacter.mechanics, Resources.Load<Sprite>("Sprites/statMechanicsIcon"));
        
        UpdateStatDetails(" ", playerCharacter.acrobatics, Resources.Load<Sprite>("Sprites/statAcrobaticsIcon"));
        
        UpdateStatDetails(" ", playerCharacter.forensics, Resources.Load<Sprite>("Sprites/statForensicsIcon"));
        
        UpdateStatDetails(" ", playerCharacter.acting, Resources.Load<Sprite>("Sprites/statActingIcon"));
        
        UpdateStatDetails(" ", playerCharacter.alchemy, Resources.Load<Sprite>("Sprites/statAlchemyIcon"));
        
        UpdateStatDetails(" ", playerCharacter.astrology, Resources.Load<Sprite>("Sprites/statAstrologyIcon"));
        
        UpdateStatDetails(" ", playerCharacter.thievery, Resources.Load<Sprite>("Sprites/statThiveryIcon"));
        
        UpdateStatDetails(" ", playerCharacter.rangedCombat, Resources.Load<Sprite>("Sprites/statRangedCombatIcon"));
        
        UpdateStatDetails(" ", playerCharacter.handToHandCombat, Resources.Load<Sprite>("Sprites/statHandToHandIcon"));
        
        UpdateStatDetails(" ", playerCharacter.etiquette, Resources.Load<Sprite>("Sprites/statEtiquetteIcon"));
        
        UpdateStatDetails(" ", playerCharacter.animism, Resources.Load<Sprite>("Sprites/statAnimismIcon"));
        
        UpdateStatDetails(" ", playerCharacter.empathy, Resources.Load<Sprite>("Sprites/statEmpathyIcon"));
        
        UpdateStatDetails(" ", playerCharacter.demonology, Resources.Load<Sprite>("Sprites/statDemonologyIcon"));
        
        UpdateStatDetails(" ", playerCharacter.stealth, Resources.Load<Sprite>("Sprites/statStealthIcon"));
        
        UpdateStatDetails(" ", playerCharacter.necromancy, Resources.Load<Sprite>("Sprites/statNecromancyIcon"));


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


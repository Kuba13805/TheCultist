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
        
        UpdateStatDetails("Perception allows you to see details that escape other people." +
                          " You perceive hidden objects, passages or events in the world.", playerCharacter.perception, Resources.Load<Sprite>("Sprites/statPerceptionIcon"));
        
        UpdateStatDetails("Occultism determines how well you know the cursed and forbidden practices of followers of dark forces." +
                          " You recognize their rituals, symbols or purposes.", playerCharacter.occultism, Resources.Load<Sprite>("Sprites/statOccultismIcon"));
        
        UpdateStatDetails("Medicine helps to treat wounds, neutralize disease symptoms and examine corpses." +
                          " You know well what happened to the deceased, and the human body has no secrets for you.", playerCharacter.medicine, Resources.Load<Sprite>("Sprites/statMedicineIcon"));
        
        UpdateStatDetails("Electrical engineering is a new field that provides you with the power to wield electricity. " +
                          "You are able to repair electrical wires or damage them, and you combine physics and chemistry with it to use it against your enemies. " +
                          "It also helps you use some new technologies.", playerCharacter.electrics, Resources.Load<Sprite>("Sprites/statElectricsIcon"));
        
        UpdateStatDetails("History is the memory of the world. It is detailed knowledge, too boring for ordinary people. You know about past events, dead people and organizations. " +
                          "You use it to avoid the mistakes of those who are no longer with us.", playerCharacter.history, Resources.Load<Sprite>("Sprites/statOccultismIcon"));
        
        UpdateStatDetails("Persuasion will help you persuade someone to your point. You are able to cozy up and sweep your eyes when needed. " +
                          "You manipulate people, draw out information and get them to follow your words.", playerCharacter.persuasion, Resources.Load<Sprite>("Sprites/statPersuasionIcon"));
        
        UpdateStatDetails("Intimidation allows you to dominate over another person. She feels fear at the mere thought of you, and you use this to your advantage. " +
                          "Orders, information or an easy way to an unattainable goal.", playerCharacter.intimidation, Resources.Load<Sprite>("Sprites/statIntimidationIcon"));
        
        UpdateStatDetails("Locksmithing is the delicate art of opening locks." +
                          " You don't need a key when you have your picks with you. No door stands locked in front of you.", playerCharacter.locksmithing, Resources.Load<Sprite>("Sprites/statLocksmithingIcon"));
        
        UpdateStatDetails("Mechanics is also called engineering. With it, you can repair simple mechanisms and know their construction and operation.", playerCharacter.mechanics, Resources.Load<Sprite>("Sprites/statMechanicsIcon"));
        
        UpdateStatDetails("Acrobatics is a circus art that will help you get into hard-to-reach places. " +
                          "You also use it from distraction or quick escape.", playerCharacter.acrobatics, Resources.Load<Sprite>("Sprites/statAcrobaticsIcon"));
        
        UpdateStatDetails("Forensic science makes it possible to better identify the objects of a crime, examine the places where it occurred, and find out the hidden motives of the perpetrator. " +
                          "It also allows you to read investigation files and know the applicable laws.", playerCharacter.forensics, Resources.Load<Sprite>("Sprites/statForensicsIcon"));
        
        UpdateStatDetails("Acting is not only an art, but also a derivative of persuasion. " +
                          "In a truly theatrical way, you draw from people what you need from them, with the addition of drama or comedy.", playerCharacter.acting, Resources.Load<Sprite>("Sprites/statActingIcon"));
        
        UpdateStatDetails("Alchemy uses the gifts of the earth to create potions and elixirs. You are familiar with herbs and decoctions. " +
                          "It allows you to use some of them in battle as non-obvious weapons.", playerCharacter.alchemy, Resources.Load<Sprite>("Sprites/statAlchemyIcon"));
        
        UpdateStatDetails("Astrology allows you to read the night sky, in which the fate of the world is written. You recognize constellations, stars and planets. " +
                          "It has a special connection with the occult and knowledge of demons.", playerCharacter.astrology, Resources.Load<Sprite>("Sprites/statAstrologyIcon"));
        
        UpdateStatDetails("Thievery is the foul art of stealing from unsuspecting passersby." +
                          " You easily take money and documents out of their pockets.", playerCharacter.thievery, Resources.Load<Sprite>("Sprites/statThiveryIcon"));
        
        UpdateStatDetails("Ranged combat determines how well you handle a revolver, rifle, shotgun or traditional crossbow.", playerCharacter.rangedCombat, Resources.Load<Sprite>("Sprites/statRangedCombatIcon"));
        
        UpdateStatDetails("Melee combat determines your proficiency with daggers, swords, axes and similar weapons.", playerCharacter.handToHandCombat, Resources.Load<Sprite>("Sprites/statHandToHandIcon"));
        
        UpdateStatDetails("Etiquette is mainly used when dealing with the city's social cream. " +
                          "You know how to behave, what to avoid and how to successfully gain the respect and approval of the noble.", playerCharacter.etiquette, Resources.Load<Sprite>("Sprites/statEtiquetteIcon"));
        
        UpdateStatDetails("Animism is an old field that combines magic with alchemy and biology. " +
                          "You know ways to communicate with animals and their souls. You use it in battle to draw on animal and plant powers. " +
                          "The most experienced can allegedly even hear the voice of plants.", playerCharacter.animism, Resources.Load<Sprite>("Sprites/statAnimismIcon"));
        
        UpdateStatDetails("Empathy allows you to recognize the interlocutor's intentions, emotions and desires. " +
                          "You are able to get into someone's skin and feel what the other person might be feeling. You simply know people.", playerCharacter.empathy, Resources.Load<Sprite>("Sprites/statEmpathyIcon"));
        
        UpdateStatDetails("Demonology is dangerous and old knowledge. You know the types of demonic creatures, their rulers, abilities, powers or purpose. " +
                          "You know how to fight them and how to get rid of them. Or perhaps dominate them?", playerCharacter.demonology, Resources.Load<Sprite>("Sprites/statDemonologyIcon"));
        
        UpdateStatDetails("Sneaking allows you to avoid unnecessary eyes. You stick to the shadows and blend in with the crowd. " +
                          "You disappear from the eyes of your pursuers and skillfully track your target.", playerCharacter.stealth, Resources.Load<Sprite>("Sprites/statStealthIcon"));
        
        UpdateStatDetails("Necromancy is magic that wields the dead, drawing their souls to your call. " +
                          "It allows you to communicate with them or use the power of death against your enemies.", playerCharacter.necromancy, Resources.Load<Sprite>("Sprites/statNecromancyIcon"));


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


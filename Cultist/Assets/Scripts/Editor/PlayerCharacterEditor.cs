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

        if (GUILayout.Button("Update"))
        {
            PlayerData playerCharacter = (PlayerData)target;
            UpdateDescriptions(playerCharacter);
        }
    }

    private void UpdateDescriptions(PlayerData playerCharacter)
    {
        playerCharacter.dexterity.statDesc = "Agility allows you to move faster and more nimbly around the city. In combat, " +
                                             "it allows you to dodge enemy attacks and affects the player character's accuracy.";
        
        playerCharacter.dexterity.statIcon = Resources.Load<Sprite>("Sprites/statDexterityIcon");

        EditorUtility.SetDirty(playerCharacter);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif
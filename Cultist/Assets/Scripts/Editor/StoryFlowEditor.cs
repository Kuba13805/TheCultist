using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Campaign))]
public class CampaignEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var campaign = (Campaign)target;
        
        DrawDefaultInspector();
        
        if (GUILayout.Button("Restart campaign"))
        {
            //campaign.ResetCampaign();
        }
        
        if (GUILayout.Button("Force complete campaign"))
        {
            //campaign.ForceCompleteCampaign();
        }
    }
}
[CustomEditor(typeof(Questline))]
public class QuestlineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var questline = (Questline)target;
        
        DrawDefaultInspector();
        
        if (GUILayout.Button("Restart questline"))
        {
            questline.RestartQuestline();
        }
        
        if (GUILayout.Button("Force complete questline"))
        {
            questline.ForceCompleteQuestline();
        }
    }
}
[CustomEditor(typeof(Quest), true)]
public class QuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var quest = (Quest)target;
        
        DrawDefaultInspector();
        
        if (GUILayout.Button("Restart quest"))
        {
            quest.RestartQuest();
        }
        
        if (GUILayout.Button("Force complete quest"))
        {
            quest.ForceCompleteQuest();
        }
    }
}
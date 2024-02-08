using UnityEngine.Serialization;

namespace UserInterfaceScripts.Dialogue
{
    [System.Serializable]
    public class DialogueRequiredQuest
    {
        public Quest requiredQuest;

        public QuestStatus questStatus;
    }

    public enum QuestStatus
    {
        NotStarted,
        Started,
        Completed,
    }
}
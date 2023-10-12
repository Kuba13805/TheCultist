using System;
using Questlines.SingleQuests;

namespace Events
{
    public class CallQuestEvents
    {
        public static event Action<QuestId> OnQuestStart;

        public static event Action<QuestId> OnQuestComplete;

        public static event Action<QuestId> OnQuestFail;

        public void StartQuest(QuestId questId)
        {
            OnQuestStart?.Invoke(questId);
        }
        public void CompleteQuest(QuestId questId)
        {
            OnQuestComplete?.Invoke(questId);
        }
        public void FailQuest(QuestId questId)
        {
            OnQuestFail?.Invoke(questId);
        }
    }
}
using UnityEngine;

public class SingleQuestPanelOnUIData : MonoBehaviour
{
    public Quest currentQuest;

    private void Start()
    {
        Quest.OnQuestCompleted += ObserveCompletedQuests;
    }

    private void OnDestroy()
    {
        Quest.OnQuestCompleted -= ObserveCompletedQuests;
    }

    private void ObserveCompletedQuests(Quest quest)
    {
        if (currentQuest == quest)
        {
            Destroy(gameObject);
        }
    }
}

namespace Questlines.SingleQuests
{
    [System.Serializable]
    public class QuestId
    {
        public string idPrefix;
        public int questNumber;

        public override string ToString()
        {
            return idPrefix + "_" + questNumber;
        }
    }
}
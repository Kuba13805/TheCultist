using NaughtyAttributes;

[System.Serializable]
public class Reward
{
    [Label("Type")][AllowNesting]
    public RewardType rewardType;

    [ShowIf("rewardType", RewardType.GiveItem)][AllowNesting][Label("Item")]
    public BaseItem rewardItem;
    
    //rewardClue
    
    [ShowIf("rewardType", RewardType.GiveMoney)][AllowNesting][Label("Money")]
    public float rewardMoney;
    
    [ShowIf("ShowStatToModify")][AllowNesting][Label("Stat")]
    public Stat statToModify;
    
    //rewardStatExp
    
    [ShowIf("rewardType", RewardType.GiveStatLevel)][AllowNesting][Label("Increase Value")]
    public int rewardStatLevel;
    
    [ShowIf("rewardType", RewardType.GiveAbility)][AllowNesting][Label("Ability")]
    public Ability rewardAbility;

    private bool ShowStatToModify()
    {
        return rewardType is RewardType.GiveStatExp or RewardType.GiveStatLevel;
    }
}
public enum RewardType
{
    GiveItem,
    GiveClue,
    GiveMoney,
    GiveStatExp,
    GiveStatLevel,
    GiveAbility,
}

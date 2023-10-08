using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    #region Events

    public static event Action<BaseItem> OnAddItemToInventory;

    public static event Action<BaseItem> OnRemoveItemFromInventory;

    public static event Action<float> OnAddMoneyToPlayer;

    public static event Action<float> OnRemoveMoneyFromPlayer; 

    public static event Action<Stat, int> OnTestPlayerStat;

    public static event Action<string> OnChangePlayerNickname;

    public static event Action<int> OnHealPlayer;

    public static event Action<int> OnDamagePlayer;

    public static event Action OnAddClue;

    public static event Action OnAddEntryInCompendium;

    public static event Action OnEndGame;

    public static event Action OnStopPlayer;
    
    #endregion
    
    public static void AddItem(BaseItem item)
    {
        OnAddItemToInventory?.Invoke(item);
    }

    public static void RemoveItem(BaseItem item)
    {
        OnRemoveItemFromInventory?.Invoke(item);
    }

    public void AddMoney(float quantity)
    {
        OnAddMoneyToPlayer?.Invoke(quantity);
    }

    public void RemoveMoney(float quantity)
    {
        OnRemoveMoneyFromPlayer?.Invoke(quantity);
    }

    public static void TestStat(Stat stat, int value)
    {
        OnTestPlayerStat?.Invoke(stat, value);
    }

    public void ChangeNickname(string newNickname)
    {
        OnChangePlayerNickname?.Invoke(newNickname);
    }

    public void HealPlayer(int points)
    {
        OnHealPlayer?.Invoke(points);
    }

    public void DamagePlayer(int points)
    {
        OnDamagePlayer?.Invoke(points);
    }

    public void AddNewClue()
    {
        OnAddClue?.Invoke();
    }

    public void AddNewEntry()
    {
        OnAddEntryInCompendium?.Invoke();
    }

    public static void FinishGame()
    {
        OnEndGame?.Invoke();
    }

    public static void StopPlayer()
    {
        OnStopPlayer?.Invoke();   
    }
}

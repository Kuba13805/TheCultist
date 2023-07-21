using System.Collections.Generic;
using NaughtyAttributes;
using Questlines;
using UnityEngine;

namespace PlayerScripts
{
    [CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObjects/Create New Player Data", order = 1)]
    public class PlayerData : BaseCharacter
    {
        #region Skills

        [Foldout("Skills")] public Perception perception;
        
        [Foldout("Skills")] public Occultism occultism;
        
        [Foldout("Skills")] public Medicine medicine;
        
        [Foldout("Skills")] public Electrics electrics;
        
        [Foldout("Skills")] public History history;
        
        [Foldout("Skills")] public Persuasion persuasion;
        
        [Foldout("Skills")] public Intimidation intimidation;
        
        [Foldout("Skills")] public Locksmithing locksmithing;
        
        [Foldout("Skills")] public Mechanics mechanics;
        
        [Foldout("Skills")] public Acrobatics acrobatics;
        
        [Foldout("Skills")] public Forensics forensics;
        
        [Foldout("Skills")] public Acting acting;
        
        [Foldout("Skills")] public Alchemy alchemy;

        [Foldout("Skills")] public Astrology astrology;

        [Foldout("Skills")] public Thievery thievery;

        [Foldout("Skills")] public RangedCombat rangedCombat;

        [Foldout("Skills")] public HandToHandCombat handToHandCombat;

        [Foldout("Skills")] public Etiquette etiquette;

        [Foldout("Skills")] public Animism animism;

        [Foldout("Skills")] public Empathy empathy;

        [Foldout("Skills")] public Demonology demonology;

        [Foldout("Skills")] public Stealth stealth;

        [Foldout("Skills")] public Necromancy necromancy;

        #endregion

        public List<BaseItem> playerInventoryItems;

        public List<BaseItem> characterEquipment;

        public List<Ability> playerAbilities;
    }
}

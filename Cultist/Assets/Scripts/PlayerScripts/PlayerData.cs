using System;
using System.Collections.Generic;
using System.Reflection;
using NaughtyAttributes;
using UnityEngine;

namespace PlayerScripts
{
    [CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObjects/Create New Player Data", order = 1)]
    public class PlayerData : BaseCharacter
    {
        protected virtual void OnEnable()
        {
            ConfirmCharacterSelection.OnCharacterConfirmedSelection += SwitchStats;
        }

        private void SwitchStats(PlayableCharacter newStats)
        {
            CopyData(newStats);
        }

        #region Skills

        
        [HorizontalLine(color: EColor.Green)][Foldout("Skills")][AllowNesting] public Perception perception;
        
        [Foldout("Skills")][AllowNesting] public Occultism occultism;
        
        [Foldout("Skills")][AllowNesting] public Medicine medicine;
        
        [Foldout("Skills")][AllowNesting] public Electrics electrics;
        
        [Foldout("Skills")][AllowNesting] public History history;
        
        [Foldout("Skills")][AllowNesting] public Persuasion persuasion;
        
        [Foldout("Skills")][AllowNesting] public Intimidation intimidation;
        
        [Foldout("Skills")][AllowNesting] public Locksmithing locksmithing;
        
        [Foldout("Skills")][AllowNesting] public Mechanics mechanics;
        
        [Foldout("Skills")][AllowNesting] public Acrobatics acrobatics;
        
        [Foldout("Skills")][AllowNesting] public Forensics forensics;
        
        [Foldout("Skills")][AllowNesting] public Acting acting;
        
        [Foldout("Skills")][AllowNesting] public Alchemy alchemy;

        [Foldout("Skills")][AllowNesting] public Astrology astrology;

        [Foldout("Skills")][AllowNesting] public Thievery thievery;

        [Foldout("Skills")][AllowNesting] public RangedCombat rangedCombat;

        [Foldout("Skills")][AllowNesting] public HandToHandCombat handToHandCombat;

        [Foldout("Skills")][AllowNesting] public Etiquette etiquette;

        [Foldout("Skills")][AllowNesting] public Animism animism;

        [Foldout("Skills")][AllowNesting] public Empathy empathy;

        [Foldout("Skills")][AllowNesting] public Demonology demonology;

        [Foldout("Skills")][AllowNesting] public Stealth stealth;

        [Foldout("Skills")][AllowNesting] public Necromancy necromancy;

        #endregion

        [HorizontalLine(color: EColor.Green)] public List<BaseItem> playerInventoryItems;

        public List<BaseItem> characterEquipment;

        public List<Ability> playerAbilities;

        private void CopyData(PlayableCharacter newStats)
        {
            Type playableCharType = typeof(PlayableCharacter);
            Type playerDataType = typeof(PlayerData);

            FieldInfo[] playableFields = playableCharType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            FieldInfo[] playerFields = playerDataType.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo playableField in playableFields)
            {
                FieldInfo correspondingPlayerField = Array.Find(playerFields, field => field.Name == playableField.Name);

                if (correspondingPlayerField != null)
                {
                    correspondingPlayerField.SetValue(this, playableField.GetValue(newStats));
                }
            }
        }
    }
}

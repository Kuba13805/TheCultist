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

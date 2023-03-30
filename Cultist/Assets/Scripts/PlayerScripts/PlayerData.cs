using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace PlayerScripts
{
    [CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObjects/Create New Player Data", order = 1)]
    public class PlayerData : ScriptableObject
    {
        public string charName;
        public string nickname;

        [SerializeField] private int Health;
        public int health;

        [Foldout("NewAttributes")][SerializeField] private int _baseDexterity;
        [Foldout("BaseAttributes")][SerializeField] private int _baseStrength;
        [Foldout("BaseAttributes")][SerializeField] private int _basePower;
        [Foldout("BaseAttributes")][SerializeField] private int BaseCondition;
        [Foldout("BaseAttributes")][SerializeField] private int BaseWisdom;
        
        [Foldout("BaseSkills")][SerializeField] private int BasePerceptivity;
        [Foldout("BaseSkills")][SerializeField] private int BaseOccultism;
        [Foldout("BaseSkills")][SerializeField] private int BaseMedicine;
        [Foldout("BaseSkills")][SerializeField] private int BaseElectrics;
        [Foldout("BaseSkills")][SerializeField] private int BaseHistory;
        [Foldout("BaseSkills")][SerializeField] private int BasePersuasion;
        [Foldout("BaseSkills")][SerializeField] private int BaseIntimidation;
        [Foldout("BaseSkills")][SerializeField] private int BaseLocksmithing;
        [Foldout("BaseSkills")][SerializeField] private int BaseMechanics;
        [Foldout("BaseSkills")][SerializeField] private int BasePsychology;

        [Foldout("CalculatedAttributes")][EnableIf("Disable")] public int dexterity;
        [Foldout("CalculatedAttributes")][EnableIf("Disable")] public int strength;
        [Foldout("CalculatedAttributes")][EnableIf("Disable")] public int power;
        [Foldout("CalculatedAttributes")][EnableIf("Disable")] public int condition;
        [Foldout("CalculatedAttributes")][EnableIf("Disable")] public int wisdom;

        [Foldout("CalculatedSkills")][EnableIf("Disable")] public int perceptivity;
        [Foldout("CalculatedSkills")][EnableIf("Disable")] public int occultism;
        [Foldout("CalculatedSkills")][EnableIf("Disable")] public int medicine;
        [Foldout("CalculatedSkills")][EnableIf("Disable")] public int electrics;
        [Foldout("CalculatedSkills")][EnableIf("Disable")] public int history;
        [Foldout("CalculatedSkills")][EnableIf("Disable")] public int persuasion;
        [Foldout("CalculatedSkills")][EnableIf("Disable")] public int intimidation;
        [Foldout("CalculatedSkills")][EnableIf("Disable")] public int locksmithing;
        [Foldout("CalculatedSkills")][EnableIf("Disable")] public int mechanics;
        [Foldout("CalculatedSkills")][EnableIf("Disable")] public int psychology;

        public string lastLocation;

        public List<BaseItem> playerInventoryItems;

        public List<BaseItem> characterEquipment;

        protected int BaseDexterity
        {
            get => _baseDexterity;
            set => _baseDexterity = value;
        }

        public int BaseStrength
        {
            get => _baseStrength;
            set => _baseStrength = value;
        }

        protected int BasePower
        {
            get => _basePower;
            set => _basePower = value;
        }
    }
}

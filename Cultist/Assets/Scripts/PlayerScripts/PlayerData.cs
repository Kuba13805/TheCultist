using System.Collections.Generic;
using NaughtyAttributes;
using Questlines;
using UnityEngine;

namespace PlayerScripts
{
    [CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObjects/Create New Player Data", order = 1)]
    public class PlayerData : ScriptableObject
    {
        public string charName;
        public string nickname;
        public int health;
        public Sprite playerPortrait;

        [Foldout("CalculatedAttributes")] public int dexterity;
        [Foldout("CalculatedAttributes")] public int strength;
        [Foldout("CalculatedAttributes")] public int power;
        [Foldout("CalculatedAttributes")] public int condition;
        [Foldout("CalculatedAttributes")] public int wisdom;

        [Foldout("CalculatedSkills")] public int perception;
        [Foldout("CalculatedSkills")] public int occultism;
        [Foldout("CalculatedSkills")] public int medicine;
        [Foldout("CalculatedSkills")] public int electrics;
        [Foldout("CalculatedSkills")] public int history;
        [Foldout("CalculatedSkills")] public int persuasion;
        [Foldout("CalculatedSkills")] public int intimidation;
        [Foldout("CalculatedSkills")] public int locksmithing;
        [Foldout("CalculatedSkills")] public int mechanics;
        [Foldout("CalculatedSkills")] public int psychology;

        public List<BaseItem> playerInventoryItems;

        public List<BaseItem> characterEquipment;

        public List<Ability> playerAbilities;
    }
}

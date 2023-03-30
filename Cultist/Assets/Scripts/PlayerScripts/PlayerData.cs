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

        [SerializeField] private float Health;


        public float health => Health;

        [Foldout("Attributes")]
        public int dexterity;
        [Foldout("Attributes")]
        public int strength;
        [Foldout("Attributes")]
        public int power;
        [Foldout("Attributes")]
        public int condition;
        [Foldout("Attributes")]
        public int wisdom;

        [Foldout("Skills")][EnableIf("Disable")]
        public int perceptivity;
        [Foldout("Skills")][EnableIf("Disable")]
        public int occultism;
        [Foldout("Skills")][EnableIf("Disable")]
        public int medicine;
        [Foldout("Skills")][EnableIf("Disable")]
        public int electrics;
        [Foldout("Skills")][EnableIf("Disable")]
        public int history;
        [Foldout("Skills")][EnableIf("Disable")]
        public int persuasion;
        [Foldout("Skills")][EnableIf("Disable")]
        public int intimidation;
        [Foldout("Skills")][EnableIf("Disable")]
        public int locksmithing;
        [Foldout("Skills")][EnableIf("Disable")]
        public int mechanics;
        [Foldout("Skills")][EnableIf("Disable")]
        public int psychology;

        public string lastLocation;

        public List<BaseItem> playerInventoryItems;

        public List<BaseItem> characterEquipment;
        
    }
}

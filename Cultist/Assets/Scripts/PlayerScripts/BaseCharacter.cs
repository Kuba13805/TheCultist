using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts
{
    [CreateAssetMenu(fileName = "NewBaseData", menuName = "ScriptableObjects/Create New Base Data")]
    public class BaseCharacter : ScriptableObject
    {
        public string charName;
        public string nickname;
        public int health;
        public Sprite playerPortrait;

        #region Attributes

        [HorizontalLine(color: EColor.Green)] [Foldout("Attributes")] public Dexterity dexterity;
        
        [Foldout("Attributes")] public Strength strength;
        
        [Foldout("Attributes")] public Power power;
        
        [Foldout("Attributes")] public Condition condition;
        
        [Foldout("Attributes")] public Wisdom wisdom;
        
        #endregion
    }
}
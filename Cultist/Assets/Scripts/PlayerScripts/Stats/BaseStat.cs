using UnityEngine;
using System.Runtime.Serialization;
using NaughtyAttributes;
using UnityEngine.Serialization;

[System.Serializable]
public class BaseStat
{
    [AllowNesting] public int statValue;
    [HideInInspector] public Sprite statIcon;
    [HideInInspector] [FormerlySerializedAs("StatDesc")] public string statDesc;

    // Konstruktor klasy
    public BaseStat(int value, Sprite icon, string description)
    {
        statValue = value;
        statIcon = icon;
    }
}

// Nowy interfejs do obsługi serializacji i deserializacji klas dziedziczących z BaseStat
public interface IStatSerializationSurrogate
{
    void GetObjectData(object obj, SerializationInfo info, StreamingContext context);
    object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);
}

[System.Serializable]
public class StatContainer : ISerializationCallbackReceiver
{
    [SerializeField]
    private BaseStat baseStat;

    // Dodatkowe pola do przechowania danych podczas serializacji
    [SerializeField]
    private int statValue;
    [SerializeField]
    private Sprite statIcon;
    [SerializeField]
    private string statDescription;

    // Implementacja interfejsu ISerializationCallbackReceiver
    public void OnBeforeSerialize()
    {
        // Przed serializacją przypisz dane z BaseStat do pomocniczych pól
        statValue = baseStat.statValue;
        statIcon = baseStat.statIcon;
        statDescription = baseStat.statDesc;
    }

    public void OnAfterDeserialize()
    {
        // Po deserializacji utwórz nowy obiekt BaseStat z pomocniczych pól
        baseStat = new BaseStat(statValue, statIcon, statDescription);
    }
}

// Przykładowa klasa dziedzicząca z BaseStat
[System.Serializable]
public class DerivedStat : BaseStat, IStatSerializationSurrogate
{
    // Dodatkowe pola dla klasy pochodnej
    [HideInInspector] public bool isDerived;

    public DerivedStat(int value, Sprite icon, string description, bool derived) : base(value, icon, description)
    {
        statDesc = "test desc";
        isDerived = derived;
    }

    // Implementacja interfejsu IStatSerializationSurrogate dla klasy pochodnej
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        DerivedStat derivedStat = (DerivedStat)obj;
        info.AddValue("isDerived", derivedStat.isDerived);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        DerivedStat derivedStat = (DerivedStat)obj;
        derivedStat.isDerived = info.GetBoolean("isDerived");
        return derivedStat;
    }
    public override string ToString()
    {
        return statValue.ToString();
    }
}

[System.Serializable]
public class Dexterity : DerivedStat
{
    public Dexterity(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Dex desc";
        
        statIcon = Resources.Load<Sprite>("Sprites/statDexterityIcon");
    }
}
[System.Serializable]
public class Strength : DerivedStat
{
    public Strength(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Str desc";

        statIcon = Resources.Load<Sprite>("Sprites/statStrengthIcon");
    }
}
[System.Serializable]
public class Power : DerivedStat
{
    public Power(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Pow desc";

        statIcon = Resources.Load<Sprite>("Sprites/statPowerIcon");
    }
}
[System.Serializable]
public class Condition : DerivedStat
{
    public Condition(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Con desc";

        statIcon = Resources.Load<Sprite>("Sprites/statConditionIcon");
    }
}
[System.Serializable]
public class Wisdom : DerivedStat
{
    public Wisdom(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Wis desc";

        statIcon = Resources.Load<Sprite>("Sprites/statWisdomIcon");
    }
}


[System.Serializable]
public class Perception : DerivedStat
{
    public Perception(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Perception desc";

        statIcon = Resources.Load<Sprite>("Sprites/statPerceptionIcon");
    }
}

[System.Serializable]
public class Occultism : DerivedStat
{
    public Occultism(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Occultism desc";

        statIcon = Resources.Load<Sprite>("Sprites/statOccultismIcon");
    }
}
[System.Serializable]
public class Medicine : DerivedStat
{
    public Medicine(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Medicine desc";

        statIcon = Resources.Load<Sprite>("Sprites/statMedicineIcon");
    }
}
[System.Serializable]
public class Electrics : DerivedStat
{
    public Electrics(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Electrics desc";

        statIcon = Resources.Load<Sprite>("Sprites/statElectricsIcon");
    }
}
[System.Serializable]
public class History : DerivedStat
{
    public History(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "History desc";

        statIcon = Resources.Load<Sprite>("Sprites/statHistoryIcon");
    }
}
[System.Serializable]
public class Persuasion : DerivedStat
{
    public Persuasion(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Persuasion desc";

        statIcon = Resources.Load<Sprite>("Sprites/statPersuasionIcon");
    }
}
[System.Serializable]
public class Intimidation : DerivedStat
{
    public Intimidation(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Intimidation desc";

        statIcon = Resources.Load<Sprite>("Sprites/statIntimidationIcon");
    }
}
[System.Serializable]
public class Locksmithing : DerivedStat
{
    public Locksmithing(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Locksmithing desc";

        statIcon = Resources.Load<Sprite>("Sprites/statLocksmithingIcon");
    }
}
[System.Serializable]
public class Mechanics : DerivedStat
{
    public Mechanics(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Mechanics desc";

        statIcon = Resources.Load<Sprite>("Sprites/statMechanicsIcon");
    }
}
[System.Serializable]
public class Acrobatics : DerivedStat
{
    public Acrobatics(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Acrobatics desc";

        statIcon = Resources.Load<Sprite>("Sprites/statAcrobaticsIcon");
    }
}
[System.Serializable]
public class Forensics : DerivedStat
{
    public Forensics(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Forensics desc";

        statIcon = Resources.Load<Sprite>("Sprites/statForensicsIcon");
    }
}
[System.Serializable]
public class Acting : DerivedStat
{
    public Acting(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Acting desc";

        statIcon = Resources.Load<Sprite>("Sprites/statActingIcon");
    }
}
[System.Serializable]
public class Alchemy : DerivedStat
{
    public Alchemy(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Alchemy desc";

        statIcon = Resources.Load<Sprite>("Sprites/statAlchemyIcon");
    }
}
[System.Serializable]
public class Astrology : DerivedStat
{
    public Astrology(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Astrology desc";

        statIcon = Resources.Load<Sprite>("Sprites/statAstrologyIcon");
    }
}
[System.Serializable]
public class Thievery : DerivedStat
{
    public Thievery(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Thievery desc";

        statIcon = Resources.Load<Sprite>("Sprites/statThieveryIcon");
    }
}
[System.Serializable]
public class RangedCombat : DerivedStat
{
    public RangedCombat(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "RangedCombat desc";

        statIcon = Resources.Load<Sprite>("Sprites/statRangedCombatIcon");
    }
}
[System.Serializable]
public class HandToHandCombat : DerivedStat
{
    public HandToHandCombat(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "HandToHandCombat desc";

        statIcon = Resources.Load<Sprite>("Sprites/statHandToHandCombatIcon");
    }
}
[System.Serializable]
public class Etiquette : DerivedStat
{
    public Etiquette(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Etiquette desc";

        statIcon = Resources.Load<Sprite>("Sprites/statEtiquetteIcon");
    }
}
[System.Serializable]
public class Animism : DerivedStat
{
    public Animism(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Etiquette desc";

        statIcon = Resources.Load<Sprite>("Sprites/statAnimismIcon");
    }
}
[System.Serializable]
public class Empathy : DerivedStat
{
    public Empathy(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Empathy desc";

        statIcon = Resources.Load<Sprite>("Sprites/statEmpathyIcon");
    }
}
[System.Serializable]
public class Demonology : DerivedStat
{
    public Demonology(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Demonology desc";

        statIcon = Resources.Load<Sprite>("Sprites/statDemonologyIcon");
    }
}
[System.Serializable]
public class Stealth : DerivedStat
{
    public Stealth(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Stealth desc";

        statIcon = Resources.Load<Sprite>("Sprites/statStealthIcon");
    }
}
[System.Serializable]
public class Necromancy : DerivedStat
{
    public Necromancy(int value, Sprite icon, string description, bool derived) : base(value, icon, description, derived)
    {
        statDesc = "Necromancy desc";

        statIcon = Resources.Load<Sprite>("Sprites/statNecromancyIcon");
    }
}
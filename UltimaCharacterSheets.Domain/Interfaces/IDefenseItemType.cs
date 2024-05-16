using UltimaCharacterSheets.Enums;

namespace UltimaCharacterSheets.Interfaces;

public interface IDefenseItemType : IItemType
{
    public CharacterAttribute? DefenseDie { get; }
    public int DefenseModifier { get; }
    public CharacterAttribute? MagicalDefenseDie { get; }
    public int MagicalDefenseModifier { get; }
    public int InitiativeModifier { get; }
}
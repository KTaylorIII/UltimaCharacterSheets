using UltimaCharacterSheets.Enums;

namespace UltimaCharacterSheets.Interfaces;

public interface IWeaponType : IItemType
{
    public WeaponCategory Category { get; }
    public Handedness Handedness { get; }
    public CharacterAttribute FirstAccuracyAttribute { get; }
    public CharacterAttribute SecondAccuracyAttribute { get; }
    public byte AccuracyModifier { get; }
    public byte DamageModifier { get; }
    public bool BreaksAfterAttack { get; }
}
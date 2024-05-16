using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using UltimaCharacterSheets.Enums;
using UltimaCharacterSheets.Interfaces;

namespace UltimaCharacterSheets.Models;

public class WeaponType : ITimeIndexable, IWeaponType
{
    [BindNever]
    [ValidateNever]
    public Guid Id { get; set; }

    [BindNever]
    [ValidateNever]
    public DateTimeOffset DateTimeCreated { get; set; }

    [MaxLength(64, ErrorMessage = "Name must not exceed 64 characters.")]
    public string Name { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "Zenit cost must be greater than or equal to zero.")]
    public long ZenitCost { get; set; }

    public WeaponCategory Category { get; set; }
    public Handedness Handedness { get; set; }
    public CharacterAttribute FirstAccuracyAttribute { get; set; }

    public CharacterAttribute SecondAccuracyAttribute { get; set; }

    [Range(byte.MinValue, byte.MaxValue, ErrorMessage = "Alert an administrator if this error crops up.")]
    public byte AccuracyModifier { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Damage modifier must be greater than or equal to zero.")]
    public byte DamageModifier { get; set; }

    public DamageType DamageType { get; set; }

    public bool BreaksAfterAttack { get; set; }
    public int Weight { get; set; }
    public virtual IEnumerable<Weapon> Weapons { get; set; }
}
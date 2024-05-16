using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using UltimaCharacterSheets.Enums;
using UltimaCharacterSheets.Interfaces;

namespace UltimaCharacterSheets.Models;

public class ArmorType : ITimeIndexable, IDefenseItemType
{
    [BindNever]
    [ValidateNever]
    public Guid Id { get; set; }

    [BindNever]
    [ValidateNever]
    [DataType(DataType.DateTime)]
    public DateTimeOffset DateTimeCreated { get; set; }

    public string Name { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "Zenit cost must be greater than or equal to zero.")]
    public long ZenitCost { get; set; }

    public CharacterAttribute? DefenseDie { get; set; }

    public int DefenseModifier { get; set; }

    public CharacterAttribute? MagicalDefenseDie { get; set; }

    public int MagicalDefenseModifier { get; set; }

    public int InitiativeModifier { get; set; }
    public int Weight { get; set; }
}
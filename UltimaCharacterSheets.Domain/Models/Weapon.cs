using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using UltimaCharacterSheets.Interfaces;

namespace UltimaCharacterSheets.Models;

public class Weapon : ITimeIndexable, IItem
{
    [BindNever]
    [ValidateNever]
    public Guid Id { get; set; }
    [BindNever]
    [ValidateNever]
    [DataType(DataType.DateTime)]
    public DateTimeOffset DateTimeCreated { get; set; }
    public string? Nickname { get; set; }
    [ForeignKey("WeaponType")]
    public Guid WeaponTypeId { get; set; }
    public virtual WeaponType WeaponType { get; set; }
    [ForeignKey("Owner")]
    public Guid? OwnerId { get; set; }
    public virtual Character? Owner { get; set; }
    [ForeignKey("Creator")]
    public Guid? CreatorId { get; set; }
    public virtual Character? Creator { get; set; }
    public long? ZenitCostOverride { get; set; }

    public int? AccuracyModifierOverride { get; set; }

    public int? DamageModifierOverride { get; set; }

    public DamageType? DamageTypeOverride { get; set; }

    public int? WeightOverride { get; set; }
}
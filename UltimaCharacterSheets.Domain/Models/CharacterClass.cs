using System.ComponentModel.DataAnnotations;
using UltimaCharacterSheets.Interfaces;

namespace UltimaCharacterSheets.Models;

public class CharacterClass : ITimeIndexable
{
    public Guid Id { get; set; }

    public DateTimeOffset DateTimeCreated { get; set; }

    [MaxLength(64, ErrorMessage = "Name must not exceed 64 characters.")]
    public string Name { get; set; }
    [MaxLength(1024, ErrorMessage = "Descriptions must not exceed 1024 characters.")]
    public string Description { get; set; }
    [Range(6, 20, ErrorMessage = "Dexterity cannot be higher than 20 or less than 6.")]
    public byte StartingDexterityDice { get; set; }
    [Range(6, 20, ErrorMessage = "Insight cannot be higher than 20 or less than 6.")]
    public byte StartingInsightDice { get; set; }
    [Range(6, 20, ErrorMessage = "Might cannot be higher than 20 or less than 6.")]
    public byte StartingMightDice { get; set; }
    [Range(6, 20, ErrorMessage = "Willpower cannot be higher than 20 or less than 6.")]
    public byte StartingWillpowerDice { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Starting Zenits must be positive or zero.")]
    public long StartingZenits { get; set; }

    public bool CanEquipMartialMeleeWeapons { get; set; }
    public bool CanEquipMartialRangedWeapons { get; set; }
    public bool CanEquipMartialArmors { get; set; }
    public bool CanEquipMartialShields { get; set; }

}
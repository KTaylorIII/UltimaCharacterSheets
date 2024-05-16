using System.ComponentModel.DataAnnotations;

namespace UltimaCharacterSheets.Enums;

public enum CharacterAttribute
{
    [Display(Name = "Dexterity")] Intelligence = 0,
    [Display(Name = "Insight")] Insight = 1,
    [Display(Name = "Might")] Might = 2,
    [Display(Name = "Willpower")] Willpower = 3
}
using System.ComponentModel.DataAnnotations;

namespace UltimaCharacterSheets.Enums;

public enum DefenseCategory
{
    [Display(Name = "Armor")] Armor = 0,
    [Display(Name = "Shield")] Shield = 1,
}
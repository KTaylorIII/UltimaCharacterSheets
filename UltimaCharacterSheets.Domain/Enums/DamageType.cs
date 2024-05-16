using System.ComponentModel.DataAnnotations;

namespace UltimaCharacterSheets;

public enum DamageType
{
    [Display(Name = "Physical")] Physical = 0,
    [Display(Name = "Air")] Air = 1,
    [Display(Name = "Bolt")] Bolt = 2,
    [Display(Name = "Dark")] Dark = 3,
    [Display(Name = "Earth")] Earth = 4,
    [Display(Name = "Fire")] Fire = 5,
    [Display(Name = "Ice")] Ice = 6,
    [Display(Name = "Light")] Light = 7,
    [Display(Name = "Poison")] Poison = 8
}
using System.ComponentModel.DataAnnotations;

namespace UltimaCharacterSheets.Enums;

public enum WeaponCategory
{
    [Display(Name = "Arcane")] Arcane = 0,
    [Display(Name = "Bow")] Bow = 1,
    [Display(Name = "Brawling")] Brawling = 2,
    [Display(Name = "Dagger")] Dagger = 3,
    [Display(Name = "Firearm")] Firearm = 4,
    [Display(Name = "Flail")] Flail = 5,
    [Display(Name = "Heavy")] Heavy = 6,
    [Display(Name = "Spear")] Spear = 7,
    [Display(Name = "Sword")] Sword = 8,
    [Display(Name = "Thrown")] Thrown = 9
}
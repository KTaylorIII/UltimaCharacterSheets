using System.ComponentModel.DataAnnotations;

namespace UltimaCharacterSheets.Enums;

public enum Handedness
{
    [Display(Name = "One-handed")] OneHanded = 0,
    [Display(Name = "Two-handed")] TwoHanded = 1,
}
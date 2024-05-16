using UltimaCharacterSheets.Models;

namespace UltimaCharacterSheets.Interfaces;

public interface IItem
{
    Guid Id { get; set; }

    Character? Owner { get; set; }
    Character? Creator { get; set; }
    string? Nickname { get; set; }
    long? ZenitCostOverride { get; set; }
    int? WeightOverride { get; set; }
}
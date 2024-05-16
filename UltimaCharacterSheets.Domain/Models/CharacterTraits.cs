using System.ComponentModel.DataAnnotations.Schema;
using UltimaCharacterSheets.Interfaces;

namespace UltimaCharacterSheets.Models;

public class CharacterTraits : ITimeIndexable
{
    public Guid Id { get; set; }
    public DateTimeOffset DateTimeCreated { get; set; }

    [ForeignKey("Character")]
    public Guid CharacterId { get; set; }
    public Character Character { get; set; }
    public string Identity { get; set; }
    public string Theme { get; set; }
    public string Origin { get; set; }
}
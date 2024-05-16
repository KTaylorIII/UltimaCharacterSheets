using System.ComponentModel.DataAnnotations.Schema;
using UltimaCharacterSheets.Interfaces;

namespace UltimaCharacterSheets.Models;

public class CharacterClassLevel : ITimeIndexable
{
    public Guid Id { get; set; }
    public DateTimeOffset DateTimeCreated { get; set; }

    [ForeignKey("Character")]
    public Guid CharacterId { get; set; }
    public virtual Character Character { get; set; }

    [ForeignKey("CharacterClass")]
    public Guid CharacterClassId { get; set; }
    public virtual CharacterClass CharacterClass { get; set; }

}
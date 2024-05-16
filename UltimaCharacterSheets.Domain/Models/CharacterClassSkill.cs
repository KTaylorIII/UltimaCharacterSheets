using UltimaCharacterSheets.Interfaces;

namespace UltimaCharacterSheets.Models;

public class CharacterClassSkill : ITimeIndexable
{
    public Guid Id { get; set; }
    public DateTimeOffset DateTimeCreated { get; set; }
    public string Name { get; set; }
}
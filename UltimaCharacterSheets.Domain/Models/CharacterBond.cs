using System.ComponentModel.DataAnnotations.Schema;
using UltimaCharacterSheets.Interfaces;

namespace UltimaCharacterSheets.Models;

public class CharacterBond : ITimeIndexable
{
    public Guid Id { get; set; }
    public DateTimeOffset DateTimeCreated { get; set; }
    public bool Admiration { get; set; }
    public bool Inferiority { get; set; }
    public bool Loyalty { get; set; }

    public bool Mistrust { get; set; }
    public bool Affection { get; set; }
    public bool Hatred { get; set; }

    [ForeignKey("Subject")]
    public Guid SubjectId { get; set; }
    public virtual Character Subject { get; set; }

    [ForeignKey("Target")]
    public Guid TargetId { get; set; }
    public virtual Character Target { get; set; }
}
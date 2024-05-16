namespace UltimaCharacterSheets.Interfaces;

public interface ITimeIndexable
{
    public Guid Id { get; }

    public DateTimeOffset DateTimeCreated { get; }

}
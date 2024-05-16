using UltimaCharacterSheets.Interfaces;

namespace UltimaCharacterSheets;

public interface IItemType : ITimeIndexable
{
    public string Name { get; }
    public long ZenitCost { get; }

    public int Weight { get; }
}
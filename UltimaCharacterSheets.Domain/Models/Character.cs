using UltimaCharacterSheets.Interfaces;

namespace UltimaCharacterSheets.Models;

public class Character : ITimeIndexable
{
    public Guid Id { get; set; }
    public DateTimeOffset DateTimeCreated { get; set; }

    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string PronounPair { get; set; }
    public int Age { get; set; }

    public CharacterTraits CharacterTraits { get; set; }
    public Weapon? EquippedWeapon { get; set; }

    public Armor? EquippedArmor { get; set; }

    public Shield? EquippedShield { get; set; }

    public virtual IEnumerable<Weapon> Weapons { get; set; }

    public virtual IEnumerable<Armor> Armors { get; set; }

    public virtual IEnumerable<Shield> Shields { get; set; }
    public virtual IEnumerable<IItem> Inventory { get => Weapons.Select(w => w as IItem).Concat(Armors.Select(a => a as IItem)).Concat(Shields.Select(s => s as IItem)); }

    public virtual IEnumerable<CharacterBond> BondedTo { get; set; } = [];
    public virtual IEnumerable<CharacterBond> BondedFrom { get; set; } = [];
}
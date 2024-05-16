using Microsoft.EntityFrameworkCore;
using UltimaCharacterSheets.Models;

namespace UltimaCharacterSheets.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterBond> CharacterBonds { get; set; }
    public DbSet<CharacterTraits> CharacterTraits { get; set; }
    public DbSet<WeaponType> WeaponTypes { get; set; }
    public DbSet<ArmorType> ArmorTypes { get; set; }
    public DbSet<ShieldType> ShieldTypes { get; set; }
    public DbSet<Weapon> Weapons { get; set; }
    public DbSet<Armor> Armors { get; set; }
    public DbSet<Shield> Shields { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new CharacterBondEntityTypeConfig().Configure(builder.Entity<CharacterBond>());
        new CharacterTraitEntityTypeConfig().Configure(builder.Entity<CharacterTraits>());
    }
}
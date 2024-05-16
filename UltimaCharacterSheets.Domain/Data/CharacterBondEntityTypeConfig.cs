using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltimaCharacterSheets.Models;

namespace UltimaCharacterSheets.Data;

public class CharacterBondEntityTypeConfig : IEntityTypeConfiguration<CharacterBond>
{
    public void Configure(EntityTypeBuilder<CharacterBond> builder)
    {
        builder.HasOne(m => m.Subject).WithMany(m => m.BondedTo).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(m => m.Target).WithMany(m => m.BondedFrom).OnDelete(DeleteBehavior.Cascade);
    }
}
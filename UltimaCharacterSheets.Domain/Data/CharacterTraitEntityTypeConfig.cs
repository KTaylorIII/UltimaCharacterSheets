using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltimaCharacterSheets.Models;

namespace UltimaCharacterSheets.Data;

public class CharacterTraitEntityTypeConfig : IEntityTypeConfiguration<CharacterTraits>
{
    public void Configure(EntityTypeBuilder<CharacterTraits> builder)
    {
        builder.HasOne(b => b.Character).WithOne(b => b.CharacterTraits).OnDelete(DeleteBehavior.Cascade);
    }
}
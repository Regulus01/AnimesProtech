using AnimesProtech.Domain.Entities;
using AnimesProtech.Infra.Data.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimesProtech.Infra.Data.Maps;

public class AnimeMap : BaseEntityMap<Anime>
{
    public override void Configure(EntityTypeBuilder<Anime> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Nome)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(e => e.Diretor)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(e => e.Resumo)
               .HasMaxLength(80)
               .IsRequired();

        builder.ToTable("Anime", "Animes");
    }
}
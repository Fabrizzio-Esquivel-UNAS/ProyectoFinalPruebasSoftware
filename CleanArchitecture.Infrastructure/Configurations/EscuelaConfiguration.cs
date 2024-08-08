using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class EscuelasConfiguration : IEntityTypeConfiguration<Escuela>
{
    public void Configure(EntityTypeBuilder<Escuela> builder)
    {
        builder
            .Property(user => user.Nombre)
            .IsRequired()
            .HasMaxLength(MaxLengths.Escuela.Nombre);

        builder.HasData(new Escuela(
            Ids.Seed.EscuelaId,
            Ids.Seed.FacultadId,
            "Admin Escuela"));
    }
}
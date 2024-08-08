using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class FacultadesConfiguration : IEntityTypeConfiguration<Facultad>
{
    public void Configure(EntityTypeBuilder<Facultad> builder)
    {
        
        builder
            .Property(user => user.Nombre)
            .IsRequired()
            .HasMaxLength(MaxLengths.Facultad.Nombre);

        builder.HasData(new Facultad(
            Ids.Seed.FacultadId,
            "Admin Facultad"));
        
    }
}
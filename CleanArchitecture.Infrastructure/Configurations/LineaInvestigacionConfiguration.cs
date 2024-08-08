using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class LineaInvestigacionConfiguration : IEntityTypeConfiguration<LineaInvestigacion>
{
    public void Configure(EntityTypeBuilder<LineaInvestigacion> builder)
    {
        builder
            .Property(user => user.Nombre)
            .IsRequired()
            .HasMaxLength(MaxLengths.LineaInvestigacion.Nombre);

        builder.HasData(new LineaInvestigacion(
            Ids.Seed.LineaInvestigacionId,
            Ids.Seed.FacultadId,
            Ids.Seed.GrupoInvestigacionId,
            "Admin Linea de Investigacion"));
    }
}
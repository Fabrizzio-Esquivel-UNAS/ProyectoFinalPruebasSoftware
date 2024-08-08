using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Infrastructure.Configurations;
public sealed class HistorialCoordinadorConfiguration : IEntityTypeConfiguration<HistorialCoordinador>
{
    public void Configure(EntityTypeBuilder<HistorialCoordinador> builder)
    {
        builder
            .Property(historialcoordinador => historialcoordinador.UserId)
            .IsRequired();

        builder
            .Property(historialcoordinador => historialcoordinador.GrupoInvestigacionId)
            .IsRequired();

        builder.HasData(new HistorialCoordinador(
            Ids.Seed.HistorialCoordinadorId,
            Ids.Seed.AdminUserId,
            Ids.Seed.GrupoInvestigacionId,
            new DateTime(2000, 12, 12, 12, 0, 0),
            new DateTime(2001, 12, 12, 12, 0, 0)));
    }
}

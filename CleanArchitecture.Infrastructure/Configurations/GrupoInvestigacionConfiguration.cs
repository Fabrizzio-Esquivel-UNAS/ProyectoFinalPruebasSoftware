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
public sealed class GrupoInvestigacionConfiguration : IEntityTypeConfiguration<GrupoInvestigacion>
{
    public void Configure(EntityTypeBuilder<GrupoInvestigacion> builder)
    {
            builder
                .Property(user => user.Nombre)
                .IsRequired()
                .HasMaxLength(MaxLengths.GrupoInvestigacion.Nombre);

            builder.HasData(new GrupoInvestigacion(
                Ids.Seed.GrupoInvestigacionId,
                "Admin Grupo de Investigacion"));
    }
}

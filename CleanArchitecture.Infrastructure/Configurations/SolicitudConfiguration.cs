using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class SolicitudConfiguration : IEntityTypeConfiguration<Solicitud>
{
    public void Configure(EntityTypeBuilder<Solicitud> builder)
    {
        
        builder
            .Property(user => user.NumeroTesis)
            .IsRequired()
            .HasMaxLength(MaxLengths.Solicitud.NumeroTesis);

        builder.HasData(new Solicitud(
            Ids.Seed.SolicitudId,
            Ids.Seed.AsesoradoUserId,
            Ids.Seed.AsesorUserId,
            1,
            "Lorem ipsum",
            SolicitudStatus.Pendiente
            ));
        
    }
}
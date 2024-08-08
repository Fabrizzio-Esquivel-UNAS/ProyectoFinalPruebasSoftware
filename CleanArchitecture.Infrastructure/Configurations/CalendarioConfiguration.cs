using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class CalendarioConfiguration : IEntityTypeConfiguration<Calendario>
{
    public void Configure(EntityTypeBuilder<Calendario> builder)
    {
    }
}
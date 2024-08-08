using System;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.Contratos;

public sealed class ContratoViewModel
{
    public Guid Id { get; set; }
    public Guid SolicitudId { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFinal { get; set; } = null;

    public static ContratoViewModel FromContrato(Contrato contrato)
    {
        return new ContratoViewModel
        {
            Id = contrato.Id,
            SolicitudId = contrato.SolicitudId,
            FechaInicio = contrato.FechaInicio,
            FechaFinal = contrato.FechaFinal,
        };
    }
}
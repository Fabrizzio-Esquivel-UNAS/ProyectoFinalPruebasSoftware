using CleanArchitecture.Domain.Enums;
using System;

namespace CleanArchitecture.Domain.Commands.Citas.CreateCita;

public sealed class CreateOrUpdateCitaCommand : CommandBase
{
    private static readonly CreateOrUpdateCitaCommandValidation s_validation = new();

    public string EventoId { get; }
    public Guid AsesorUserId { get; }
    public string AsesoradoEmail { get; }
    public DateTime FechaCreacion {  get; }
    public DateTime FechaInicio { get; }
    public DateTime FechaFin { get; }
    public CitaEstado Estado { get; }

    public CreateOrUpdateCitaCommand(
        Guid citaId, 
        string eventoId, 
        Guid asesorUserId, 
        string asesoradoEmail,
        DateTime fechaCreacion,
        DateTime fechaInicio,
        DateTime fechaFin,
        CitaEstado estado = CitaEstado.Inasistido) : base(citaId)
    {
        Estado = estado;
        AsesorUserId = asesorUserId;
        AsesoradoEmail = asesoradoEmail;
        EventoId = eventoId;
        FechaCreacion = fechaCreacion;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}
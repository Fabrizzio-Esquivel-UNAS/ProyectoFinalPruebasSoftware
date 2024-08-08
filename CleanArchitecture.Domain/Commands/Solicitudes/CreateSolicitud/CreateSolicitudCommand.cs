using System;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;

public sealed class CreateSolicitudCommand : CommandBase
{
    private static readonly CreateSolicitudCommandValidation s_validation = new();
    public Guid SolicitudId { get; }

    public Guid AsesoradoUserId { get; }
    public Guid AsesorUserId { get; }
    public int NumeroTesis { get; }
    public string Mensaje {  get; }


    public CreateSolicitudCommand(Guid solicitudId, Guid asesoradoUserId,
        Guid asesorUserId,
        int numeroTesis,
        string mensaje) : base(solicitudId)
    {
        SolicitudId = solicitudId;
        AsesoradoUserId = asesoradoUserId;
        AsesorUserId = asesorUserId;
        NumeroTesis = numeroTesis;
        Mensaje = mensaje;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}
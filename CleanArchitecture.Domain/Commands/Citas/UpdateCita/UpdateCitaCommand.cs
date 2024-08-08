using CleanArchitecture.Domain.Enums;
using System;

namespace CleanArchitecture.Domain.Commands.Citas.UpdateCita;

public sealed class UpdateCitaCommand : CommandBase
{
    private static readonly UpdateCitaCommandValidation s_validation = new();

    public CitaEstado Estado { get; }
    public string DesarrolloAsesor { get; }
    public string DesarrolloAsesorado {  get; }
    public UpdateCitaCommand(
        Guid citaId, 
        CitaEstado estado, 
        string desarrolloAsesor, 
        string desarrolloAsesorado) : base(citaId)
    {
        Estado = estado;
        DesarrolloAsesor = desarrolloAsesor;
        DesarrolloAsesorado = desarrolloAsesorado;
        
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}
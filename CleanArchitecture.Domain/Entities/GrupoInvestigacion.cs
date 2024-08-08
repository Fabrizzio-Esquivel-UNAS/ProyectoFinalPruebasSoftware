using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities;

public class GrupoInvestigacion : Entity
{
    public string Nombre { get; set; }

    public virtual ICollection<HistorialCoordinador> HistorialCoordinadores { get; set; } = new HashSet<HistorialCoordinador>();

    public virtual ICollection<LineaInvestigacion> LineasInvestigacion { get; set; } = new HashSet<LineaInvestigacion>();

    public GrupoInvestigacion(
        Guid id,
        string nombre) : base(id)
    {
        Nombre = nombre;
    }
}
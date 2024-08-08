using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;

public class Facultad : Entity
{
    public string Nombre { get; set; }

    public virtual ICollection<Escuela> Escuelas { get; set; } = new HashSet<Escuela>();

    public virtual ICollection<LineaInvestigacion> LineasInvestigacion { get; set; } = new HashSet<LineaInvestigacion>();

    public Facultad(
        Guid id,
        string nombre) : base(id)
    {
        Nombre = nombre;
    }

    public void SetName(string nombre)
    {
        Nombre = nombre;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities;

public class Escuela : Entity
{
    public string Nombre { get; set; }
    
    public Guid FacultadId { get; private set; }
    public virtual Facultad Facultad { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

    public Escuela(
        Guid id,
        Guid facultadId,
        string nombre) : base(id)
    {
        FacultadId = facultadId;
        Nombre = nombre;
    }
    public void SetName(string nombre)
    {
        Nombre = nombre;
    }

    public void SetFacultad(Guid facultadId)
    {
        FacultadId = facultadId;
    }

}
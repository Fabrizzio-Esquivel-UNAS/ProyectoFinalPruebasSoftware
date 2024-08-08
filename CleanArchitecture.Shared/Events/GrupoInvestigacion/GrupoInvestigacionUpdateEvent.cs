using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Shared.Events.GrupoInvestigacion;
public sealed class GrupoInvestigacionUpdatedEvent : DomainEvent
{
    public string Nombre { get; set; }

    public GrupoInvestigacionUpdatedEvent(Guid grupoinvestigacionId, string nombre) : base(grupoinvestigacionId)
    {
        Nombre = nombre;
    }
}

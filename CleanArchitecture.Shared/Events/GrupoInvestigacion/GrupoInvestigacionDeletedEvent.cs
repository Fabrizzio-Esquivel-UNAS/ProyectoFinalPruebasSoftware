using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Shared.Events.GrupoInvestigacion;
public sealed class GrupoInvestigacionDeletedEvent : DomainEvent
{
    public GrupoInvestigacionDeletedEvent(Guid grupoinvestigacionId) : base(grupoinvestigacionId)
    {
    }
}

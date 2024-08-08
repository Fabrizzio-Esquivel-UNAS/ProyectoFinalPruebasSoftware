using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ViewModels.GruposInvestigacion;

public sealed record UpdateGrupoInvestigacionViewModel(
    Guid Id,
    string Nombre,
    Guid? CoordinadorUserId=null)
{
}

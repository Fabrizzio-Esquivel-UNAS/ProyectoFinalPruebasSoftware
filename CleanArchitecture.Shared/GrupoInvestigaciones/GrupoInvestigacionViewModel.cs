using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Shared.GrupoInvestigacion;
public sealed record GrupoInvestigacionViewModel(
    Guid Id,
    string Nombre);
using System;

namespace CleanArchitecture.Application.ViewModels.LineasInvestigacion;

public sealed record CreateLineaInvestigacionViewModel(string Nombre, Guid FacultadId, Guid GrupoInvestigacionId);
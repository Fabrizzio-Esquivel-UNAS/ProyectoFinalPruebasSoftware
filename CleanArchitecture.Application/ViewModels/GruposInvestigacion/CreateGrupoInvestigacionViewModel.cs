using System;

namespace CleanArchitecture.Application.ViewModels.GruposInvestigacion;

public sealed record CreateGrupoInvestigacionViewModel(string Nombre, Guid? CoordinadorUserId = null);

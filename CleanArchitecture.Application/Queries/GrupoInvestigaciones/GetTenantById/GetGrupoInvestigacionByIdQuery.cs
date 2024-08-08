using System;
using CleanArchitecture.Application.ViewModels.GruposInvestigacion;
using MediatR;

namespace CleanArchitecture.Application.Queries.GruposInvestigacion.GetGrupoInvestigacionById;

public sealed record GetGrupoInvestigacionByIdQuery(Guid GrupoInvestigacionId) : IRequest<GrupoInvestigacionViewModel?>;
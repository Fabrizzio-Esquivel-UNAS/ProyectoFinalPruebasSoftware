using System;
using CleanArchitecture.Application.ViewModels.LineasInvestigacion;
using MediatR;

namespace CleanArchitecture.Application.Queries.LineasInvestigacion.GetLineaInvestigacionById;

public sealed record GetLineaInvestigacionByIdQuery(Guid LineaInvestigacionId) : IRequest<LineaInvestigacionViewModel?>;
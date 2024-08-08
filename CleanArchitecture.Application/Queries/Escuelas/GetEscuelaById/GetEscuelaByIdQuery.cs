using System;
using CleanArchitecture.Application.ViewModels.Escuelas;
using MediatR;

namespace CleanArchitecture.Application.Queries.Escuelas.GetEscuelasById;

public sealed record GetEscuelasByIdQuery(Guid EscuelasId) : IRequest<EscuelaViewModel?>;
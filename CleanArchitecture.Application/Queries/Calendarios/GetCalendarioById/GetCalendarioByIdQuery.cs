using System;
using CleanArchitecture.Application.ViewModels.Calendarios;
using MediatR;

namespace CleanArchitecture.Application.Queries.Calendarios.GetCalendarioById;

public sealed record GetCalendarioByIdQuery(Guid CalendarioId) : IRequest<CalendarioViewModel?>;
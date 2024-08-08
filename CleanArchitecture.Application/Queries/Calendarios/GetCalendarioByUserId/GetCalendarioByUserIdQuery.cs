using System;
using CleanArchitecture.Application.ViewModels.Calendarios;
using MediatR;

namespace CleanArchitecture.Application.Queries.Calendarios.GetCalendarioByUserId;

public sealed record GetCalendarioByUserIdQuery(Guid UserId) : IRequest<CalendarioViewModel?>;
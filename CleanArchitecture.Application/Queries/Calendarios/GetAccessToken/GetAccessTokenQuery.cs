using System;
using CleanArchitecture.Application.ViewModels.Calendarios;
using MediatR;

namespace CleanArchitecture.Application.Queries.Calendarios.GetAccessToken;

public sealed record GetAccessTokenQuery(Guid UserId) : IRequest<CalendarioViewModel?>;
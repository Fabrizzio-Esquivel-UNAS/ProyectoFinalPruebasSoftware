using System;
using CleanArchitecture.Application.ViewModels.Solicitudes;
using MediatR;

namespace CleanArchitecture.Application.Queries.Solicitudes.GetSolicitudById;

public sealed record GetSolicitudByIdQuery(Guid SolicitudId) : IRequest<SolicitudViewModel?>;
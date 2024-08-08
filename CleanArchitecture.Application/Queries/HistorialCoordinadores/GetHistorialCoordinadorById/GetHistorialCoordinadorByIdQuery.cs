using System;
using CleanArchitecture.Application.ViewModels.HistorialCoordinadores;
using MediatR;

namespace CleanArchitecture.Application.Queries.HistorialCoordinadores.GetHistorialCoordinadorById;

public sealed record GetHistorialCoordinadorByIdQuery(Guid HistorialCoordinadorId) : IRequest<HistorialCoordinadorViewModel?>;
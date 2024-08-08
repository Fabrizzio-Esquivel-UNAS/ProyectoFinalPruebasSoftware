using System;
using CleanArchitecture.Application.ViewModels.Citas;
using MediatR;

namespace CleanArchitecture.Application.Queries.Citas.GetCitaById;

public sealed record GetCitaByIdQuery(Guid CitaId) : IRequest<CitaViewModel?>;
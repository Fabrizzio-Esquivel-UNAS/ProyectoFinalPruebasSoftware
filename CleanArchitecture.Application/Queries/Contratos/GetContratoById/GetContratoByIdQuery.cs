using System;
using CleanArchitecture.Application.ViewModels.Contratos;
using MediatR;

namespace CleanArchitecture.Application.Queries.Contratos.GetContratoById;

public sealed record GetContratoByIdQuery(Guid ContratoId) : IRequest<ContratoViewModel?>;